using System.Collections.Generic;
using GameControllers.Player;
using Photon.Pun;
using PlayersData;
using UnityEngine;

namespace GameControllers.Spawners
{
    public class PlayerSpawner : MonoBehaviourPun
    {
        [SerializeField] private List<Transform> _spawnPoints = new();
        [SerializeField] private GameObject _player1Prefab;
        [SerializeField] private GameObject _player2Prefab;

        private void Awake()
        {
            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            var myActorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
            var isFirstPlayer = true;

            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                if (myActorNumber < player.ActorNumber)
                    isFirstPlayer = false;
            }

            Vector3 position;

            if (isFirstPlayer)
            {   
                position = GetSpawnPosition(0);
                var newPlayer = PhotonNetwork.Instantiate(_player1Prefab.name, position, Quaternion.identity).GetComponent<SkinInitializer>();
                newPlayer.Init(isFirstPlayer);
            }
            else 
            {
                position = GetSpawnPosition(1);
                var newPlayer = PhotonNetwork.Instantiate(_player2Prefab.name, position, Quaternion.identity).GetComponent<SkinInitializer>();
                newPlayer.Init(isFirstPlayer);
            }
        }

        private Vector3 GetSpawnPosition(int indexPoint)
        {
            var spawnPoint = _spawnPoints[indexPoint];
            var position = new Vector3(
                spawnPoint.position.x, 
                spawnPoint.position.y, 
                0);

            return position;
        }
    }
}

