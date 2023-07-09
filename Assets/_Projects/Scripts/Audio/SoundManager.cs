using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GMTK
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;
        public static SoundManager instance;
        public float musicVolume = 1f;
        public float sfxVolume = 1f;
        void Awake()
        {
            instance = this;
            musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
            sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        }

        public void SetMusicVolume()
        {
            Debug.Log("Set Music Volume");
            float volume = musicSlider.value;
            musicVolume = volume;
            PlayerPrefs.SetFloat("MusicVolume", volume);
        }

        public void SetSFXVolume()
        {
            float volume = sfxSlider.value;
            sfxVolume = volume;
            PlayerPrefs.SetFloat("SFXVolume", volume);
        }

        public void PlaySFX(AudioClip clip)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, sfxVolume);
        }
    }
}
