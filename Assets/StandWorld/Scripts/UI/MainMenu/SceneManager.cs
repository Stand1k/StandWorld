using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace StandWorld.UI.MainMenu
{
    public enum SceneIndex
    {
        MainMenu,
        Game,
    }
    
    public class SceneManager : MonoBehaviour
    {
        public static readonly string GameVersion = "main";
        public static SceneManager instance;
        public GameObject transitionCanvas;
        public GameObject loadingScreen;
        public ProgressBar progressBar;
        public Sprite[] backgrounds;
        public Image backgroundImage;
        public TextMeshProUGUI tipsText;
        public CanvasGroup alphaCanvas;
        public string[] tips;
        
        private int _tipsCount;
        private float _totalSceneProgress;

        private List<AsyncOperation> sceneLoading = new List<AsyncOperation>();

        private void Awake()
        {
            instance = this;
            progressBar.current = 0f;
            loadingScreen.SetActive(false);
            transitionCanvas.SetActive(false);
            
            DontDestroyOnLoad(loadingScreen);
            DontDestroyOnLoad(transitionCanvas);
            DontDestroyOnLoad(instance);
        }

        public void LoadGame()
        {
            backgroundImage.sprite = backgrounds[Random.Range(0, backgrounds.Length)];
            loadingScreen.gameObject.SetActive(true);

            sceneLoading.Add(UnityEngine.SceneManagement.SceneManager.LoadSceneAsync((int) SceneIndex.Game, LoadSceneMode.Single));

            foreach (AsyncOperation asyncOperation in sceneLoading)
            {
                asyncOperation.allowSceneActivation = false;
            }
            
            StartCoroutine(GenerateTips());
            StartCoroutine(GetSceneLoadProgress());
        }

        public IEnumerator GetSceneLoadProgress()
        {
            for (int i = 0; i < sceneLoading.Count; i++)
            {
                while (!sceneLoading[i].isDone)
                {
                    _totalSceneProgress = 0f;

                    foreach (AsyncOperation asyncOperation in sceneLoading)
                    {
                        _totalSceneProgress += asyncOperation.progress;
                    }

                    DOTween.To(x => progressBar.current = x, progressBar.current, 
                        (_totalSceneProgress / (sceneLoading.Count - (sceneLoading.Count * 0.1f)) * 100f) + 1f, 0.5f);

                    if (progressBar.current >= 100)
                    {
                        yield return new WaitForSeconds(5f);
                        transitionCanvas.SetActive(true);
                        yield return new WaitForSeconds(1f);
                        break;
                    }
                    
                    yield return null;
                }
            }

            foreach (AsyncOperation asyncOperation in sceneLoading)
            {
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
            Destroy(loadingScreen);
        }

        public IEnumerator GenerateTips()
        {
            _tipsCount = Random.Range(0, tips.Length);
            tipsText.text = tips[_tipsCount];

            while (loadingScreen.activeInHierarchy)
            {
                yield return new WaitForSeconds(5f);

                alphaCanvas.DOFade(0f, 0.5f);

                yield return new WaitForSeconds(0.5f);

                _tipsCount++;

                if (_tipsCount >= tips.Length)
                {
                    _tipsCount = 0;
                }

                tipsText.text = tips[_tipsCount];

                alphaCanvas.DOFade(1f, 1f);
            }
        }
    }
}
