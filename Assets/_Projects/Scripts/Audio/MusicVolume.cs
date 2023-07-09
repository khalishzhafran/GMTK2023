using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK
{
    public class MusicVolume : MonoBehaviour
    {
        private AudioSource audioSource;
        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            audioSource.volume = SoundManager.instance.musicVolume;
        }
    }
}
