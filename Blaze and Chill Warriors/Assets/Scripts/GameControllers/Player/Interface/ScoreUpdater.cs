using System;
using DG.Tweening;
using GameControllers.GameEntites.Balls;
using Photon.Pun;
using PlayersData;
using TMPro;
using UnityEngine;

namespace GameControllers.Player.Interface
{
    public class ScoreUpdater : MonoBehaviourPun
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private AudioSource _increaseScoreSound;
        [SerializeField] private AudioSource _decreaseScoreSound;
        private int _currentScore;
        private Color _startColorText = new Color(0.9960784f, 0.9921569f, 0.654902f);
        private bool _isGameOver;
        private const int ratingValue = 23;
        private const int WinningScoreThreshold = 30;

        public static Action<PhotonView> OnEndGame;

        private void OnEnable()
        {
            Ball.OnUpdateScore += UpdateScoreText;
        }

        private void OnDisable()
        {
            Ball.OnUpdateScore -= UpdateScoreText;
        }

        private void UpdateScoreText(int score)
        {
            if (!photonView.IsMine) return;

            if (score > 0)
            {
                _increaseScoreSound.Play();
                photonView.RPC("SendAnimationScoreText", RpcTarget.All, 0f, 1f, 0f);
            }
            else if (score < 0)
            {
                _decreaseScoreSound.Play();
                photonView.RPC("SendAnimationScoreText", RpcTarget.All, 1f, 0f, 0f);
            }

            if (_currentScore < WinningScoreThreshold && !_isGameOver)
            {
                _currentScore += score;
                _scoreText.text = $"{_currentScore}";
            }

            if (_currentScore >= WinningScoreThreshold && !_isGameOver)
            {
                _isGameOver = true;
                SaveRating();
            }
                
            photonView.RPC("SendProgressScore", RpcTarget.Others, _currentScore);
        }

        private void SaveRating()
        {
            var currentRating = PlayerPrefs.GetInt(PlayerDataKeys.PlayerRatingKey) + ratingValue;
            PlayerPrefs.SetInt(PlayerDataKeys.PlayerRatingKey, currentRating);

            var bestRating = PlayerPrefs.GetInt(PlayerDataKeys.PlayerBestRatingKey);

            if (currentRating > bestRating)
                PlayerPrefs.SetInt(PlayerDataKeys.PlayerBestRatingKey, currentRating);

            photonView.RPC("SendChangeRating", RpcTarget.Others, ratingValue);
            InterfaceInitializer.PhotonView.RPC("SendGameOver", RpcTarget.All);
            
        }

        [PunRPC]
        private void SendAnimationScoreText(float r, float g, float b)
        {
            DOTween.Sequence()
                .Append(_scoreText.DOColor(new Color(r, g, b), 0.5f))
                .Append(_scoreText.DOColor(_startColorText, 0.5f));
        }

        [PunRPC]
        private void SendProgressScore(int currentScore)
        {
            _scoreText.text = $"{currentScore}";
        }

        [PunRPC]
        private void SendChangeRating(int ratingValue)
        {
            var currentRating = PlayerPrefs.GetInt(PlayerDataKeys.PlayerRatingKey);

            if (currentRating - ratingValue < 0)
                PlayerPrefs.SetInt(PlayerDataKeys.PlayerRatingKey, 0);
            else 
                PlayerPrefs.SetInt(PlayerDataKeys.PlayerRatingKey, currentRating - ratingValue);
        }

        [PunRPC]
        private void SendGameOver()
        {
            OnEndGame.Invoke(InterfaceInitializer.PhotonView);
        }
    }
}

