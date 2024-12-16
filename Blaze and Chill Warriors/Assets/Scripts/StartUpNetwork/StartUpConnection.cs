using System.Collections;
using GameLoaders;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace StartUpNetwork
{
    public class StartUpConnection : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_Text _loadingText;

        private void Awake()
        {   
            ConnectToNetwork();
        }

        private void ConnectToNetwork()
        {
            if (IsInternetAvailable())
            {
                StartCoroutine(StartLoadingTextAnimation());
                PhotonNetwork.ConnectUsingSettings();
            }
            else
            {
                _loadingText.text = "Check your internet connection";
            }
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            LoadingScreenController.Instance.ChangeScene("MainMenu");
        }

        private bool IsInternetAvailable()
        {
            return Application.internetReachability != NetworkReachability.NotReachable;
        }

        private IEnumerator StartLoadingTextAnimation()
        {
            while (true)
            {
                _loadingText.text = $"Connecting.";
                yield return new WaitForSeconds(0.3f);

                _loadingText.text = $"Connecting..";
                yield return new WaitForSeconds(0.3f);

                _loadingText.text = $"Connecting...";
                yield return new WaitForSeconds(0.3f);
            }
        } 
    }
}

