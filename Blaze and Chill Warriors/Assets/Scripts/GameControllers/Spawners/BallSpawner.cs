using System.Collections;
using System.Collections.Generic;
using GameControllers.FactoryControllers;
using Photon.Pun;
using UnityEngine;

namespace GameControllers.Spawners
{
    public class BallSpawner : MonoBehaviour
    {
        [SerializeField] private RegularBallFactory _regularBallFactory;
        [SerializeField] private ScoreDecreaseBallFactory _scoreDecreaseBallFactory;
        [SerializeField] private ExplosionBallFactory _explosionBallFactory;
        [SerializeField] private StunBallFactory _stunBallFactory;
        [SerializeField] private List<Transform> _spawnPoints = new();
        private Queue<Transform> _queueSpawnPoints = new();
        private float delaySpawn = 5f;
        private float _currentTime;

        private void Start()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                _queueSpawnPoints = new Queue<Transform> (_spawnPoints);
                StartCoroutine(SpawnBalls());
            }
        }

        private IEnumerator SpawnBalls()
        {
            while (true)
            {
                if (_currentTime >= 150f)
                {
                    FourthFirstWave();
                }
                else if (_currentTime >= 100f)
                {
                    ThirdFirstWave();
                    delaySpawn = 4f;
                }
                else if (_currentTime >= 50f)
                {
                    SecondFirstWave();
                }
                else
                {
                    StartFirstWave();
                }

                yield return new WaitForSeconds(delaySpawn);
                _currentTime += delaySpawn;
            }
        }

        private Transform GetSpawnPoint()
        {
            var spawnPoint = _queueSpawnPoints.Dequeue();
            _queueSpawnPoints.Enqueue(spawnPoint);

            return spawnPoint;
        }

        private void StartFirstWave()
        {
            _regularBallFactory.GetBall(GetSpawnPoint());
        }

        private void SecondFirstWave()
        {
            _regularBallFactory.GetBall(GetSpawnPoint());
            _scoreDecreaseBallFactory.GetBall(GetSpawnPoint());
        }

        private void ThirdFirstWave()
        {
            _regularBallFactory.GetBall(GetSpawnPoint());
            _scoreDecreaseBallFactory.GetBall(GetSpawnPoint());
            _explosionBallFactory.GetBall(GetSpawnPoint());
            _stunBallFactory.GetBall(GetSpawnPoint());
        }

        private void FourthFirstWave()
        {
            _regularBallFactory.GetBall(GetSpawnPoint());
            _scoreDecreaseBallFactory.GetBall(GetSpawnPoint());
            _explosionBallFactory.GetBall(GetSpawnPoint());
            _stunBallFactory.GetBall(GetSpawnPoint());
        }
    }
}

