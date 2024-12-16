using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MainMenuControllers.Store
{
    public class WolfsDataContainer : MonoBehaviour
    {
        public static List<RedWolfData> RedWolfsData { get; private set; }
        public static List<BlueWolfData> BlueWolfsData { get; private set; }

        public static void LoadWolfsData()
        {
            RedWolfsData = Resources.LoadAll<RedWolfData>("ScriptableObjectData/RedWolfs")
                .OrderBy(x => x.Index)
                .ToList();

            BlueWolfsData = Resources.LoadAll<BlueWolfData>("ScriptableObjectData/BlueWolfs")
                .OrderBy(x => x.Index)
                .ToList();
        }
    }
}

