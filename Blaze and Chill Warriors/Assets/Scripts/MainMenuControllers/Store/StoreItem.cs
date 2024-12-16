using System;
using TMPro;
using UnityEngine;

namespace MainMenuControllers.Store
{
    public class StoreItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _selectText;
        [SerializeField] private GameObject _chosenBlock;
        [SerializeField] private GameObject _lockedBlock;
        [SerializeField] private TMP_Text _lockedText;
        [SerializeField] private CharacterData _characterData;
        private Action<string, TypeWolf> _chooseItem;

        public void Init(Action<string, TypeWolf> reselectItem)
        {
            _chooseItem = reselectItem;
            LoadItemData();
        }

        public void LoadItemData()
        {
            _chosenBlock.SetActive(false);
            _lockedBlock.SetActive(false);
            _selectText.enabled = false;

            if (_characterData.TypeStateStoreItem == TypeStateStoreItem.IsLocked)
            {
                _lockedBlock.SetActive(true);
                _lockedText.text = $"You need to reach {_characterData.RatingToOpen} rating";
            }
            else if (_characterData.TypeStateStoreItem == TypeStateStoreItem.IsChosen)
            {
                _chosenBlock.SetActive(true);
            }
            else if (_characterData.TypeStateStoreItem == TypeStateStoreItem.IsOpen)
            {
                _selectText.enabled = true;
            }
        }

        public void UnchooseItem()
        {
            _chosenBlock.SetActive(false);
            _lockedBlock.SetActive(false);
            _selectText.enabled = true;
        }

        public void ClickChooseItem()
        {
            if (!(_characterData.TypeStateStoreItem == TypeStateStoreItem.IsLocked))
            {
                _chooseItem.Invoke(_characterData.IndexEntryKey, _characterData.TypeWolf);

                PlayerPrefs.SetInt(_characterData.IndexEntryKey, _characterData.Index);
                _chosenBlock.SetActive(true);
                _selectText.enabled = false;
            }
        }
    }
}

