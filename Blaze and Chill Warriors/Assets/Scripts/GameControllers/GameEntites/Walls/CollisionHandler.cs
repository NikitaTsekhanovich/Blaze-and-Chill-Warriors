using GameControllers.GameEntites.Balls;
using Photon.Pun;
using UnityEngine;

namespace GameControllers.GameEntites.Walls
{
    public class CollisionHandler : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent<Ball>(out var ball))
            {
                if (PhotonNetwork.IsMasterClient)
                    ball.ReturnBall();
            }
        }
    }
}

