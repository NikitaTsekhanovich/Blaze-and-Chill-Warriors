using System;
using GameControllers.GameEntites.Balls.Properties;
using Photon.Pun;
using UnityEngine;

namespace GameControllers.GameEntites.Balls
{
    public class StunBall : Ball, ICanInitBall<StunBall>
    {
        private const float stunTime = 2.5f;
        private Action<StunBall> _returnAction { get; set; }

        public static Action<float> OnStanPlayer;

        public void Init(Vector3 startPosition, Action<StunBall> returnAction)
        {
            transform.position = startPosition;
            _returnAction = returnAction;
        }

        public override void ReturnBall()
        {
            _returnAction.Invoke(this);
        }

        public override void DestroyBall(bool isOwnerBullet)
        {
            OnStanPlayer.Invoke(stunTime);
            base.DestroyBall(isOwnerBullet);
        }

        [PunRPC]
        public override void SyncActiveStateBall(bool isActive)
        {
            if (!isActive)
                transform.position = new Vector3(0, 20, 0);
            
            _ballImage.enabled = true;
            _circleCollider.enabled = true;
            gameObject.SetActive(isActive);
        }
    }
}

