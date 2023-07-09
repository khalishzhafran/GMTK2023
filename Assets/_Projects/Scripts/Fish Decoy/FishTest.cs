using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GMTK.Fisherman;
using GMTK.EventSystem;
using GMTK.Cameras;

namespace GMTK
{
    public class FishTest : MonoBehaviour
    {
        private Fisher fisher;
        public bool isCaught = false;
        private Rigidbody2D rb;
        public float speed = 1f;
        public float fishPower = 1;

        [Space(10)]
        [Header("Mood Gain")]
        public float moodGainSpeedPerSecond = 1f;
        public float maxGain = 10f;
        void Awake()
        {
            fisher = FindObjectOfType<Fisher>();
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Moving();
        }

        private void Moving()
        {
            if (isCaught)
            {
                transform.position = transform.parent.position;

                Vector2 horizontal = Input.GetAxis("Horizontal") * Vector2.right * speed;
                Vector2 vertical = Input.GetAxis("Vertical") * Vector2.up * speed;

                rb.velocity = horizontal + vertical;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Hook" && !isCaught)
            {
                isCaught = true;

                other.gameObject.GetComponent<Reeling>().GetFish(rb);

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
