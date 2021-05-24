using System;
using UnityEngine;

namespace StandWorld.Audio
{
    public class AudioSettings : MonoBehaviour // TODO : Rework audio system
    {
        private FMOD.Studio.Bus _master;
        private FMOD.Studio.Bus _music;
        private FMOD.Studio.Bus _sfx;
        public static float MasterVolume;
        public static float MusicVolume;
        public static float SFXVolume;

        private void Awake()
        {
            _music = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
            _sfx = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");
            _master = FMODUnity.RuntimeManager.GetBus("bus:/");
        }

        void Update()
        {
            _master.setVolume(MasterVolume);
            _music.setVolume(MusicVolume);
            _sfx.setVolume(SFXVolume);
        }

        public void MasterVolumeLevel(float newMasterVolume)
        {
            MasterVolume = newMasterVolume;
        }
        
        public void MusicVolumeLevel(float newMusicVolume)
        {
            MusicVolume = newMusicVolume;
        }
        
        public void SFXVolumeLevel(float newSFXVolume)
        {
            SFXVolume = newSFXVolume;
        }
    }
}
