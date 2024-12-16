using System.Collections.Generic;
using UnityEngine;

namespace MainMenuControllers.Store
{
    public class CharacterSelectionController : MonoBehaviour
    {
        [SerializeField] private List<StoreItem> _blueWolfsItems = new();
        [SerializeField] private List<StoreItem> _redWolfsItems = new();

        private void Start()
        {
            OpenStore();
        }

        private void OpenStore()
        {            
            RefreshItemsData(_blueWolfsItems);
            RefreshItemsData(_redWolfsItems);
        }

        private void RefreshItemsData(List<StoreItem> storeItems)
        {
            foreach (var storeItem in storeItems)
                storeItem.Init(ReselectItem);
        }

        private void ReselectItem(string indexKey, TypeWolf typeWolf)
        {
            if (typeWolf == TypeWolf.BlueWolf)
                _blueWolfsItems[PlayerPrefs.GetInt(indexKey)].UnchooseItem();
            else if (typeWolf == TypeWolf.RedWolf)
                _redWolfsItems[PlayerPrefs.GetInt(indexKey)].UnchooseItem();
        }
    }
}

