using Photon.Pun;
using UnityEngine;

namespace GameControllers.Player
{
    public class AnimationsController : MonoBehaviourPun
    {
        [SerializeField] private Animator _animator;

        public void StartStunAnimation(bool isStunned)
        {
            _animator.SetBool("IsStunned", !isStunned);
        }
    }
}

