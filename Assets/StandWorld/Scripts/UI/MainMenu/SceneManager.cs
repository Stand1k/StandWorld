﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using StandWorld.Game;
using TMPro;
using UniRx;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

namespace StandWorld.UI.MainMenu
{
    public enum SceneIndex
    {
        MainMenu,
        Game
    }
    
    public class SceneManager : MonoBehaviour
    {
        public static readonly string GameVersion = "main";
        public static SceneManager instance;
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
        }

        public void LoadGame()
        {
            backgroundImage.sprite = backgrounds[UnityEngine.Random.Range(0, backgrounds.Length)];
            loadingScreen.gameObject.SetActive(true);

            sceneLoading.Add(UnityEngine.SceneManagement.SceneManager.LoadSceneAsync((int) SceneIndex.Game, LoadSceneMode.Single));
            
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

                    progressBar.current = (_totalSceneProgress / sceneLoading.Count) * 100f;

                    yield return null;
                }
            }
            
            loadingScreen.gameObject.SetActive(false);
        }

        public IEnumerator GenerateTips()
        {
            _tipsCount = UnityEngine.Random.Range(0, tips.Length);
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