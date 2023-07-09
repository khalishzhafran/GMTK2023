using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK
{
    public class Emote : MonoBehaviour
    {
        [SerializeField] AudioClip rageClip;
        [SerializeField] AudioClip happyClip;
        public static Emote instance;
        [SerializeField] private float emoteDuration = 1f;
        [SerializeField] private SpriteRenderer[] RageEmotes;
        [SerializeField] private SpriteRenderer[] HappyEmotes;
        void Awake()
        {
            instance = this;
        }

        public void Rage()
        {
            int index = Random.Range(0, RageEmotes.Length);
            SoundManager.instance.PlaySFX(rageClip);
            Destroy(Instantiate(RageEmotes[index], transform.position, Quaternion.identity), emoteDuration);
        }

        public void Happy()
        {
            int index = Random.Range(0, HappyEmotes.Length);
            SoundManager.instance.PlaySFX(happyClip);
            Destroy(Instantiate(HappyEmotes[index], transform.position, Quaternion.identity), emoteDuration);
        }
    }
}
