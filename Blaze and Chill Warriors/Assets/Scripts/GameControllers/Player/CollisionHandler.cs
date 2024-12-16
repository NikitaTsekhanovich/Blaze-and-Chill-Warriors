using GameControllers.GameEntites.Arrows;
using Photon.Pun;
using UnityEngine;

namespace GameControllers.Player
{
    public class CollisionHandler : MonoBehaviourPun
    {
        [SerializeField] private StunHandler _stunHandler;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent<Arrow>(out var bullet))
            {
                if (photonView.IsMine && !bullet.IsMineBullet)
                    _stunHandler.BeStunned(bullet.PlayerStunTime);
            }
        }
    }
}

