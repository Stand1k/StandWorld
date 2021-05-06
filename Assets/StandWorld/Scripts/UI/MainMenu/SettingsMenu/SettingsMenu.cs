using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StandWorld.UI.MainMenu
{
    public class SettingsMenu : MonoBehaviour
    {
        public TMP_Dropdown resolutionDropdown;
        public Toggle fullscreenToggle;

        private Resolution[] _resolutions;

        private void Start()
        {
            resolutionDropdown.ClearOptions();

            List<string> options = new List<string>();
            
            _resolutions = Screen.resolutions.Select(resolution => 
                new Resolution
                {
                    width = resolution.width, height = resolution.height
                }).Distinct().ToArray();

            int currentResolutionIndex = 0;
            for (int i = 0; i < _resolutions.Length; i++)
            {
                string option = _resolutions[i].width + " x " + _resolutions[i].height;
                options.Add(option);

                if (_resolutions[i].width == Screen.currentResolution.width &&
                    _resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }
            
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
            
            LoadSettings(currentResolutionIndex);
        }

        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = _resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
            
            if (Screen.fullScreen)
            {
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            }
            else
            {
                Screen.fullScreenMode = FullScreenMode.Windowed;
            }
        }

        public void ApplySettings()
        {
            SetResolution(resolutionDropdown.value);
            SetFullscreen(fullscreenToggle.isOn);
            SaveSettings();
        }

        public void SaveSettings()
        {
            PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
            PlayerPrefs.SetInt("FullscreenPreference", Convert.ToInt32(fullscreenToggle.isOn));
        }

        public void LoadSettings(int currentResolutionIndex)
        {
            if (PlayerPrefs.HasKey("ResolutionPreference"))
            {
                resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
            }
            else
            {
                resolutionDropdown.value = currentResolutionIndex;
            }

            if (PlayerPrefs.HasKey("FullscreenPreference"))
            {
                bool isOn = Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
                SetFullscreen(isOn);
                fullscreenToggle.isOn = isOn;
            }
            else
            {
                fullscreenToggle.isOn = true;
                Screen.fullScreen = true;
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            }
        }
    }
}
