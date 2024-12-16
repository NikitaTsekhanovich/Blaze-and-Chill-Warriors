using MainMenuControllers.Store;
using Photon.Pun;
using UnityEngine;

namespace GameControllers.Player
{
    public class SkinInitializer : MonoBehaviourPun
    {
        [SerializeField] private SpriteRenderer _body;
        [SerializeField] private SpriteRenderer _leftHand;
        [SerializeField] private SpriteRenderer _rightHand;
        [SerializeField] private SpriteRenderer _bow;

        public void Init(bool isFirstPlayer)
        {
            if (photonView.IsMine)
            {
                string keyCharacterData;

                if (isFirstPlayer)
                    keyCharacterData = CharactersDataKeys.IndexChosenRedWolfKey;
                else
                    keyCharacterData = CharactersDataKeys.IndexChosenBlueWolfKey;

                photonView.RPC("SendInitSettings", RpcTarget.All, PlayerPrefs.GetInt(keyCharacterData), isFirstPlayer);
            }
        }

        [PunRPC]
        private void SendInitSettings(int indexChatacter, bool isFirstPlayer)
        {
            InitSkinsSettings(indexChatacter, isFirstPlayer);
        }

        private void InitSkinsSettings(int indexChatacter, bool isFirstPlayer)
        {
            CharacterData characterData;

            if (isFirstPlayer)
                characterData = WolfsDataContainer.RedWolfsData[indexChatacter];
            else
                characterData = WolfsDataContainer.BlueWolfsData[indexChatacter];

            _body.sprite = characterData.Body;
            _leftHand.sprite = characterData.LeftHand;
            _rightHand.sprite = characterData.RightHand;
            _bow.sprite = characterData.Bow;
        }
    }
}

