using System;
using DG.Tweening;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace GameControllers.GameEntites.Balls
{
    public abstract class Ball : MonoBehaviourPun
    {
        [SerializeField] private Transform _directionPoint;
        [SerializeField] private float _speed;
        [SerializeField] private AudioSource _destroySound;
        [SerializeField] protected CircleCollider2D _circleCollider;
        [SerializeField] protected TMP_Text _scoreText;
        [SerializeField] protected SpriteRenderer _ballImage;
        [SerializeField] protected ParticleSystem _destroyParticle;
        protected const int minScoreValue = 1;
        protected const int maxScoreValue = 5;
        
        private const float delayDestroy = 1f;

        public bool NotReadyDestroyable { get; protected set; }
        public int ScoreValue { get; protected set; }
        public static Action<int> OnUpdateScore;

        private void Update()
        {
            Movement();
        }

        private void Movement()
        {
            transform.position = Vector3.Lerp(transform.position, _directionPoint.position, _speed * Time.deltaTime);
        }

        public virtual void DestroyBall(bool isOwnerBullet)
        {
            photonView.RPC("PreReturnBall", RpcTarget.All);

            if (isOwnerBullet)
                OnUpdateScore.Invoke(ScoreValue);

            _destroyParticle.Play();
            _destroySound.Play();
            
            DOTween.Sequence()
                .AppendInterval(delayDestroy)
                .AppendCallback(ReturnBall);
        }
        
        [PunRPC]
        public void SyncScoreBall(int scoreValue)
        {
            ScoreValue = scoreValue;

            if (scoreValue >= 0)
                _scoreText.text = $"+{ScoreValue}";
            else 
                _scoreText.text = $"{ScoreValue}";
        }

        [PunRPC]
        public void PreReturnBall()
        {
            _circleCollider.enabled = false;
            _ballImage.enabled = false;
            if (_scoreText != null) _scoreText.enabled = false;
        }

        [PunRPC]
        public abstract void SyncActiveStateBall(bool isActive);
        
        public abstract void ReturnBall();
    }
}

