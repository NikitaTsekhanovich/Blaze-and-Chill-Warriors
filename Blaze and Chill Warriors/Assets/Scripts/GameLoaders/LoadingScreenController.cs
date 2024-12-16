using System.Collections;
using DG.Tweening;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameLoaders
{
    public class LoadingScreenController : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GraphicRaycaster _loadingScreenBlockClick;
        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text _loadingText;
        [SerializeField] private Image _logo;
        [SerializeField] private Image _platform;
        private Coroutine _loadingTextAnimation;
        private const float delayAnimation = 0.7f;

        public static LoadingScreenController Instance;

        private void Start()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }

        public void ChangeScene(string nameScene)
        {
            _loadingScreenBlockClick.enabled = true;
            StartAnimationFade(nameScene);
        }

        private void StartAnimationFade(string nameScene)
        {
            _loadingTextAnimation = StartCoroutine(StartLoadingTextAnimation());
            _loadingText.DOFade(1f, delayAnimation);
            _logo.DOFade(1f, delayAnimation);
            _platform.DOFade(1f, delayAnimation);

            DOTween.Sequence()
                .Append(_background.DOFade(1f, delayAnimation))
                .AppendInterval(1.5f)
                .AppendCallback(() => LoadScene(nameScene))
                .AppendInterval(0.3f)
                .OnComplete(() => EndAnimationFade());
        }

        private void LoadScene(string nameScene)
        {
            if (nameScene != "")
                SceneManager.LoadSceneAsync(nameScene);
            Time.timeScale = 1f;
        }

        private void EndAnimationFade()
        {
            _logo.DOFade(0f, delayAnimation);
            _loadingText.DOFade(0f, delayAnimation);
            _platform.DOFade(0f, delayAnimation);

            DOTween.Sequence()
                .Append(_background.DOFade(0f, delayAnimation))
                .AppendCallback(() => StopCoroutine(_loadingTextAnimation))
                .AppendCallback(() => _loadingScreenBlockClick.enabled = false)
                .AppendCallback(() => Time.timeScale = 1f);
        }

        private IEnumerator StartLoadingTextAnimation()
        {
            while (true)
            {
                _loadingText.text = $"Loading.";
                yield return new WaitForSeconds(0.3f);

                _loadingText.text = $"Loading..";
                yield return new WaitForSeconds(0.3f);

                _loadingText.text = $"Loading...";
                yield return new WaitForSeconds(0.3f);
            }
        }
    }
}

