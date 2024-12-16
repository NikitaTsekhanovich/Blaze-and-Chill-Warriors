using MainMenuControllers.Store;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameLoaders
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance;

        private void Start()
        {             
            if (Instance == null) 
                Instance = this;  
            else 
                Destroy(this);   
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == "MainMenu")
            {
                WolfsDataContainer.LoadWolfsData();
            }
        }
    }
}

