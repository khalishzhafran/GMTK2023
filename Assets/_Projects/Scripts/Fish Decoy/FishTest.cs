using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GMTK.Fisherman;
using GMTK.EventSystem;
using GMTK.Cameras;
using System;

namespace GMTK
{
    public class FishTest : MonoBehaviour
    {
        [SerializeField] AudioClip caughtClip;
        public static int fishCount = 0;
        public bool isCaught = false;
        private bool inWater = true;
        private Rigidbody2D rb;
        public float patrolSpeed = 1f;
        public float direction = 1f;
        public float speed = 1f;
        public float fishPower = 1;
        public Vector2 timer;


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
            fishCount++;
            StartCoroutine(FishChangePatrolDirection());
        }

        private void OnDestroy()
        {
            fishCount--;
        }

        void FixedUpdate()
        {
            Moving();
            RotateToDirection();
            Patrol();
        }

        private void RotateToDirection()
        {
            if (rb.velocity != Vector2.zero)
            {
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        private void Moving()
        {
            if (isCaught)
            {
                transform.position = transform.parent.position;
                if (inWater)
                {
                    Vector2 horizontal = Input.GetAxis("Horizontal") * Vector2.right * speed;
                    Vector2 vertical = Input.GetAxis("Vertical") * Vector2.up * speed;

                    rb.velocity = horizontal + vertical;
                }
                else
                {
                    rb.velocity = Vector2.zero;
                }
            }
        }

        private void Patrol()
        {
            if (!isCaught && inWater)
            {
                rb.velocity = Vector2.right * direction * patrolSpeed;
                if (transform.position.x > 16)
                {
                    Destroy(gameObject);
                }
                else if (transform.position.x < -16)
                {
                    Destroy(gameObject);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Hook" && !isCaught)
            {
                SoundManager.instance.PlaySFX(SoundManager.instance.soundEffectSO.HookClicked);
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

            if (other.gameObject.tag == "Water")
            {
                inWater = false;
            }
        }

        private IEnumerator FishChangePatrolDirection()
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(timer.x, timer.y));
            direction *= -1;
            StartCoroutine(FishChangePatrolDirection());
        }
    }
}
