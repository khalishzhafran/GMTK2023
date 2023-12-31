using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GMTK.Fisherman;
using GMTK.EventSystem;
using GMTK.Cameras;

namespace GMTK
{
    public class Trash : MonoBehaviour
    {
        [SerializeField] AudioClip caughtClip;
        public static int trashCount = 0;
        public bool isCaught = false;
        private Rigidbody2D rb;

        [Space(10)]
        [Header("Mood Gain")]
        public float moodGainSpeedPerSecond = 1f;
        public float maxGain = 10f;
        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            trashCount++;
        }

        private void OnDestroy()
        {
            trashCount--;
        }

        private void Update()
        {
            if (isCaught)
            {
                transform.position = transform.parent.position;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Hook" && !isCaught)
            {
                SoundManager.instance.PlaySFX(SoundManager.instance.soundEffectSO.ReelIn);
                isCaught = true;

                other.gameObject.GetComponent<Reeling>().GetTrash(this);

                transform.parent = other.gameObject.transform;
                transform.position = transform.parent.position;

                CameraSwitcher.SwitchCamera();

                OnHookedObject evt = Events.OnHookedObject;
                evt.hookedObject = transform;
                evt.moodGainSpeedPerSecond = moodGainSpeedPerSecond;
                evt.maxGain = maxGain;
                EventManager.Broadcast(evt);
            }
        }
    }
}
