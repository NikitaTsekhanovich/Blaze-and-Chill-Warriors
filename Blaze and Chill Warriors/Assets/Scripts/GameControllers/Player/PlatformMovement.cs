using Photon.Pun;
using UnityEngine;

namespace GameControllers.Player
{
    public class PlatformMovement : MonoBehaviourPun
    {
        [SerializeField] private float _speed;
        private Rigidbody2D _rigidbody;
        private Vector3 _movement;
        private Vector3 _previousMovement;
        private const float upperLimitMovement = 3.8f;
        private const float lowerLimitMovement = -1f;

        private void Awake()
        {
            if (photonView.IsMine)
            {
                _rigidbody = GetComponent<Rigidbody2D>();
                _movement = Vector3.up * _speed;
            }
        }

        private void Update()
        {
            if (photonView.IsMine)
                SwitchDirection();
        }

        private void FixedUpdate()
        {
            if (photonView.IsMine)
                MovePlatform();
        }

        public void ChangeStateMove(bool isCanMove)
        {
            if (!isCanMove)
            {
                if (_movement != Vector3.zero)
                    _previousMovement = _movement;

                _movement = Vector3.zero;
            }
            else
            {
                _movement = _previousMovement;
            }
        }

        private void SwitchDirection()
        {
            if (transform.position.y >= upperLimitMovement)
                _movement = Vector3.down * _speed;
            else if (transform.position.y <= lowerLimitMovement)
                _movement = Vector3.up * _speed;
        }

        private void MovePlatform()
        {
            _rigidbody.velocity = _movement;
        }
    }
}

