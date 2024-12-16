using System;
using GameControllers.GameEntites.Balls.Properties;
using Photon.Pun;
using UnityEngine;

namespace GameControllers.GameEntites.Balls
{
    public class RegularBall : Ball, ICanInitBall<RegularBall>
    {
        private Action<RegularBall> _returnAction { get; set; }

        public void Init(Vector3 startPosition, Action<RegularBall> returnAction)
        {
            var scoreValue = UnityEngine.Random.Range(minScoreValue, maxScoreValue);
            photonView.RPC("SyncScoreBall", RpcTarget.All, scoreValue);

            transform.position = startPosition;
            _returnAction = returnAction;
        }

        public override void ReturnBall()
        {
            _returnAction.Invoke(this);
        }

        [PunRPC]
        public override void SyncActiveStateBall(bool isActive)
        {
            if (!isActive)
                transform.position = new Vector3(0, 20, 0);
            
            _ballImage.enabled = true;
            _scoreText.enabled = true;
            _circleCollider.enabled = true;
            gameObject.SetActive(isActive);
        }
    }
}

