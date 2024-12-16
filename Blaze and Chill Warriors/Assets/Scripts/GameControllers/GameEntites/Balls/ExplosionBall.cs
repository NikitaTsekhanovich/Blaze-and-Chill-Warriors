using System;
using System.Collections.Generic;
using GameControllers.GameEntites.Balls.Properties;
using Photon.Pun;
using UnityEngine;

namespace GameControllers.GameEntites.Balls
{
    public class ExplosionBall : Ball, ICanInitBall<ExplosionBall>
    {
        [SerializeField] private ParticleSystem _explosionParticle;
        [SerializeField] private AudioSource _explosionSound;
        private HashSet<Ball> _ballsInRadius = new();
        private Action<ExplosionBall> _returnAction { get; set; }

        public void Init(Vector3 startPosition, Action<ExplosionBall> returnAction)
        {
            var scoreValue = UnityEngine.Random.Range(minScoreValue, maxScoreValue);
            photonView.RPC("SyncScoreBall", RpcTarget.All, scoreValue);

            NotReadyDestroyable = false;
            transform.position = startPosition;
            _returnAction = returnAction;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent<Ball>(out var ball))
            {
                _ballsInRadius.Add(ball);
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.TryGetComponent<Ball>(out var ball))
            {
                _ballsInRadius.Remove(ball);
            }
        }

        public override void DestroyBall(bool isOwnerBullet)
        {
            NotReadyDestroyable = true;
            Explosion(isOwnerBullet);

            _explosionParticle.Play();
            _explosionSound.Play();

            base.DestroyBall(isOwnerBullet);
        }

        private void Explosion(bool isOwnerBullet)
        {
            var ballsToSedtroy = new List<Ball>(_ballsInRadius);

            foreach (var ballToDestroy in ballsToSedtroy)
            {
                if (ballToDestroy != null && !ballToDestroy.NotReadyDestroyable)
                    ballToDestroy?.DestroyBall(isOwnerBullet);
            }

            _ballsInRadius.Clear();
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

