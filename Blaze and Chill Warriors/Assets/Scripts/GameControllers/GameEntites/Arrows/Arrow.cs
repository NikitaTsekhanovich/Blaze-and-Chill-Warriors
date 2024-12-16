using System;
using GameControllers.GameEntites.Arrows.Properties;
using Photon.Pun;
using UnityEngine;

namespace GameControllers.GameEntites.Arrows
{
    public class Arrow : MonoBehaviourPun
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private OwnerArrowType _ownerArrowType;
        private float _currentTime;
        private float _rateChangeAngle;
        private float _downwardFlightAngle = -90f;
        private Action<Arrow> _returnAction;
        private const float speedForce = 10f;
        private const float lifeTime = 3f;
        private const float angleChangeMultiplier = 30f; 

        private Vector2 a;

        [field: SerializeField] public float PlayerStunTime { get; private set; }
        public bool IsMineBullet { get; private set; }

        public void Init(bool isMineBullet, Action<Arrow> returnAction, Transform throwPoint, float energyShoot)
        {
            if (_ownerArrowType == OwnerArrowType.FirstPlayer)
                _downwardFlightAngle = 90f;
            else if (_ownerArrowType == OwnerArrowType.SecondPlayer)
                _downwardFlightAngle = -90f;

            _rateChangeAngle = energyShoot * angleChangeMultiplier;
            IsMineBullet = isMineBullet;
            _returnAction = returnAction;
            transform.SetPositionAndRotation(throwPoint.position, throwPoint.rotation);
        }

        private void Update()
        {
            CheckLifeTime();
        }

        private void FixedUpdate()
        {
            MakePhysicalFall();
            TestMove();
        }

        private void TestMove()
        {
            // Debug.Log(a);
            // _rigidbody.velocity += a * Time.fixedDeltaTime * 500;
        }

        private void MakePhysicalFall()
        {
            if (_rateChangeAngle == 0) return;

            var currentAngle = _rigidbody.rotation;
            var angleDifference = Mathf.DeltaAngle(currentAngle, _downwardFlightAngle);
            var rotationStep = _rateChangeAngle * Time.fixedDeltaTime;

            var torque = Mathf.Clamp(angleDifference, -rotationStep, rotationStep);

            _rigidbody.MoveRotation(currentAngle - torque);
        }

        private void CheckLifeTime()
        {
            if (!IsMineBullet) return;
            
            _currentTime += Time.deltaTime;

            if (_currentTime >= lifeTime) 
            {
                _returnAction.Invoke(this);
                _currentTime = 0;
            }
        }

        public void Movement(float energyShoot, Vector2 directionShoot)
        {
            
            a = directionShoot * energyShoot;
            _rigidbody.velocity = a * Time.fixedDeltaTime * 400;
            // _rigidbody.velocity = directionShoot * energyShoot;
            // _rigidbody.AddForce(directionShoot * energyShoot * speedForce, ForceMode2D.Impulse);
        }

        [PunRPC]
        private void SyncActiveStateBullet(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}

