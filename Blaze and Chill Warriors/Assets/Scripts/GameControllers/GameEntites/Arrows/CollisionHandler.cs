using GameControllers.GameEntites.Balls;
using UnityEngine;

namespace GameControllers.GameEntites.Arrows
{
    public class CollisionHandler : MonoBehaviour
    {
        [SerializeField] private Arrow _bullet;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent<Ball>(out var ball))
            {
                if (_bullet.IsMineBullet)
                    ball.DestroyBall(_bullet.IsMineBullet);
            }
        }
    }
}

