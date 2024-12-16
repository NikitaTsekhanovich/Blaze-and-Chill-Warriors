using GameControllers.GameEntites.Arrows;
using GameControllers.ObjectsPool;
using Photon.Pun;
using UnityEngine;

namespace GameControllers.Player
{
    public class ShootSystem : MonoBehaviourPun
    {
        [SerializeField] private Transform _throwPoint;
        [SerializeField] private Transform _weaponPoint;
        [SerializeField] private Arrow _bullet;
        private PoolBase<Arrow> _bulletPool;
        private const int bulletPreloadCount = 10;

        private void Awake()
        {
            if (photonView.IsMine)
                _bulletPool = new PoolBase<Arrow>(Preload, GetBulletAction, ReturnBulletsAction, bulletPreloadCount);
        }

        public void Shoot(float energyShoot, float maxEnergyShoot)
        {
            if (!photonView.IsMine) return;

            var newBullet = _bulletPool.Get();
            var directionWeapon = new Vector2(_throwPoint.position.x - _weaponPoint.position.x, _throwPoint.position.y - _weaponPoint.position.y);

            newBullet.Init(photonView.IsMine, ReturnBullet, _throwPoint, maxEnergyShoot - energyShoot);
            newBullet.Movement(energyShoot, directionWeapon);
        }

        private void ReturnBullet(Arrow bullet) => _bulletPool.Return(bullet);
        
        public Arrow Preload() => PhotonNetwork.Instantiate(_bullet.name, _throwPoint.position, _throwPoint.transform.rotation).GetComponent<Arrow>();
        public void GetBulletAction(Arrow bullet) => bullet.photonView.RPC("SyncActiveStateBullet", RpcTarget.All, true);
        public void ReturnBulletsAction(Arrow bullet) => bullet.photonView.RPC("SyncActiveStateBullet", RpcTarget.All, false);
    }
}

