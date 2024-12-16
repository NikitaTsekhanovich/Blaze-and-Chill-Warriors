using GameControllers.GameEntites.Balls;
using GameControllers.GameEntites.Balls.Properties;
using GameControllers.ObjectsPool;
using Photon.Pun;
using UnityEngine;

namespace GameControllers.FactoryControllers
{
    public abstract class Factory<T> : MonoBehaviour
        where T : Ball, ICanInitBall<T>
    {
        [SerializeField] private T _ball;
        protected const int ballPreloadCount = 5;
        protected PoolBase<T> _ballsPool;

        private void Awake()
        {
            if (PhotonNetwork.IsMasterClient)
                _ballsPool = new PoolBase<T>(Preload, GetBallAction, ReturnBallAction, ballPreloadCount);
        }

        public T GetBall(Transform spawnPoint)
        {
            var newBall = _ballsPool.Get();
            newBall.Init(spawnPoint.position, ReturnBall);

            return newBall;
        }

        private void ReturnBall(T ball) => _ballsPool.Return(ball);

        public T Preload() => PhotonNetwork.Instantiate(_ball.name, new Vector3(0, 20, 0), Quaternion.identity).GetComponent<T>();
        public void GetBallAction(T ball) => ball.photonView.RPC("SyncActiveStateBall", RpcTarget.All, true);
        public void ReturnBallAction(T ball) => ball.photonView.RPC("SyncActiveStateBall", RpcTarget.All, false);
    }
}

