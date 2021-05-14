using System;
using DG.Tweening;
using StandWorld.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace StandWorld.UI.MainMenu
{
    public class PopupWindow : MonoBehaviour
    {
        public GameObject creditsGO;
        public GameObject newWorldSettigsGO;
        public TMP_InputField inputField;
        
        private CanvasGroup _creditsCanvasGroup;
        private CanvasGroup _newWorldSettigsCanvasGroup;

        private void Awake()
        {
            _creditsCanvasGroup = creditsGO.GetComponent<CanvasGroup>();
            _newWorldSettigsCanvasGroup = newWorldSettigsGO.GetComponent<CanvasGroup>();
            _creditsCanvasGroup.alpha = 0f;
            _newWorldSettigsCanvasGroup.alpha = 0f;
            
            inputField.characterValidation = TMP_InputField.CharacterValidation.Integer;
            inputField.characterLimit = 9;
            inputField.text = Random.Range(100000000, 999999999).ToString();
            
            Settings.seed = Int32.Parse(inputField.text);
        }

        public void SeedChange()
        {
            Settings.seed = Int32.Parse(inputField.text);
            Debug.Log("Seed: " + Settings.seed);
        }

        public void SetMapSize(int mapSize)
        {
            Settings.mapSize = new Vector2Int(mapSize, mapSize);
        }
        
        public void EnableCredits()
        {
            creditsGO.SetActive(true);
            _creditsCanvasGroup.alpha = 0f;
            _creditsCanvasGroup.DOFade(1f, 0.4f);
        }

        public void EnableNewGameSettings()
        {
            newWorldSettigsGO.SetActive(true);
            _newWorldSettigsCanvasGroup.DOFade(1f, 0.2f);
        }
        
        public void DisableNewGameSettings()
        {
            _newWorldSettigsCanvasGroup.DOFade(0f, 0.2f)
                .OnComplete(
                    () =>
                    {
                        newWorldSettigsGO.SetActive(false);
                    });
        }

        public void DisableCredits()
        {
            _creditsCanvasGroup.DOFade(0f, 0.4f)
                .OnComplete(
                    () =>
                    {
                        creditsGO.SetActive(false);
                    });
        }
    }
}
