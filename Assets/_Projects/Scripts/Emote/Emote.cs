using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK
{
    public class Emote : MonoBehaviour
    {
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
            Destroy(Instantiate(RageEmotes[index], transform.position, Quaternion.identity), emoteDuration);
        }

        public void Happy()
        {
            int index = Random.Range(0, HappyEmotes.Length);
            Destroy(Instantiate(HappyEmotes[index], transform.position, Quaternion.identity), emoteDuration);
        }
    }
}
