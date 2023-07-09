using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK
{
    public class Reeling : MonoBehaviour
    {
        public static Reeling instance;
        [SerializeField] private float reelSpeed = 1f;
        [SerializeField] private float droptime;
        public bool isReeling { get; set; } = false;
        Vector3 startingPos;

        private Rigidbody2D rb;
        public Rigidbody2D fishRb { get; set; }

        private FishTest fish;
        private Hook hook;
        private Fisher.Fisher fisher;
        float fishPower;

        public int moodGain;
        private void Awake()
        {
            startingPos = transform.position;
            hook = GetComponent<Hook>();
            rb = GetComponent<Rigidbody2D>();
            fisher = FindObjectOfType<Fisher.Fisher>();

            instance = this;
        }

        private void FixedUpdate()
        {
            ReelingIn();
        }

        private void ReelingIn()
        {
            if (isReeling && fishRb != null)
            {
                FishRelease();

                Vector3 direction = (startingPos - transform.position).normalized;
                Vector2 hookPower = direction * reelSpeed;
                rb.velocity = hookPower + fishRb.velocity;
                hook.enabled = false;

                if (Vector2.Distance(transform.position, startingPos) < 0.1f)
                {
                    StopAllCoroutines();

                    hook.enabled = true;
                    isReeling = false;
                    rb.velocity = Vector2.zero;
                    GetComponent<CircleCollider2D>().enabled = true;
                    rb.gravityScale = 3;

                    fisher.ChangeMood(moodGain * 1);
                    Destroy(fishRb.gameObject, 0.1f);
                }
            }
            else if (isReeling && fishRb == null)
            {
                Vector3 direction = (startingPos - transform.position).normalized;
                rb.velocity = direction * 5;
                hook.enabled = false;
                if (Vector2.Distance(transform.position, startingPos) < 0.1f)
                {
                    StopAllCoroutines();
                    isReeling = false;
                    rb.velocity = Vector2.zero;

                    StartCoroutine(DropHook());
                }
            }
        }



        public void GetFish(Rigidbody2D fishRb)
        {
            isReeling = true;
            this.fishRb = fishRb;

            fish = fishRb.GetComponent<FishTest>();
            rb.gravityScale = 0;
            GetComponent<CircleCollider2D>().enabled = false;

            StartCoroutine(FishRelease());
            StartCoroutine(MoodChanger());
        }

        private IEnumerator DropHook()
        {
            yield return new WaitForSeconds(droptime);
            hook.enabled = true;
            GetComponent<CircleCollider2D>().enabled = true;
            rb.gravityScale = 3;
        }

        private IEnumerator FishRelease()
        {
            while (isReeling)
            {
                Debug.Log(fishRb.velocity.y);
                if (fishPower > 50)
                {
                    fish.isCaught = false;
                    if (fish.transform.position.x > transform.position.x) fishRb.velocity = Vector2.right * 5;
                    else fishRb.velocity = Vector2.left * 5;

                    Destroy(fishRb.gameObject, 3f);

                    fishRb = null;
                    fish = null;

                    StopAllCoroutines();
                }
                if (fish != null)
                {
                    if (fish.transform.position.x > transform.position.x)
                    {
                        if (fishRb.velocity.x > 0f || fishRb.velocity.y < 0f)
                        {
                            fishPower += fish.fishPower;
                            Debug.Log(fishPower);
                            yield return new WaitForSeconds(0.01f);
                        }
                        else
                        {
                            fishPower -= 0.5f;
                            yield return new WaitForSeconds(0.1f);
                        }
                    }

                    else if (fish.transform.position.x < transform.position.x)
                    {
                        if (fishRb.velocity.x < 0f || fishRb.velocity.y < 0f)
                        {
                            fishPower += fish.fishPower;
                            Debug.Log(fishPower);
                            yield return new WaitForSeconds(0.01f);
                        }
                        else
                        {
                            fishPower -= 0.5f;
                            yield return new WaitForSeconds(0.1f);
                        }
                    }
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(0.1f);
            }
        }

        private IEnumerator MoodChanger()
        {
            while (isReeling)
            {
                moodGain += fish.moodGain;
                if (moodGain > fish.MaxMood)
                    moodGain = fish.MaxMood;
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
