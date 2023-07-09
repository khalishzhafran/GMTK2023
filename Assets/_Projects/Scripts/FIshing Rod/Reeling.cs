using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GMTK
{
    public class Reeling : MonoBehaviour
    {
        [SerializeField] private Slider reelSlider;
        [SerializeField] private float reelSpeed = 1f;
        public bool isReeling { get; set; } = false;
        Vector3 startingPos;

        private Rigidbody2D rb;
        public Rigidbody2D fishRb { get; set; }

        private FishTest fish;
        private Hook hook;
        private Fisherman.Fisher fisher;
        float fishPower;

        private void Awake()
        {
            startingPos = transform.position;
            hook = GetComponent<Hook>();
            rb = GetComponent<Rigidbody2D>();
            fisher = FindObjectOfType<Fisherman.Fisher>();
        }

        private void FixedUpdate()
        {
            reelSlider.value = fishPower;
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
                    hook.enabled = true;
                    isReeling = false;
                    rb.velocity = Vector2.zero;
                    GetComponent<CircleCollider2D>().enabled = true;
                    rb.gravityScale = 3;

                    Destroy(fishRb.gameObject, 0.1f);
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
        }

        private IEnumerator FishRelease()
        {
            while (isReeling)
            {
                if (fish.transform.position.x > transform.position.x)
                {
                    Debug.Log("ikan di kanan");
                    if (fishRb.velocity.x > 0f || fishRb.velocity.y > 0f)
                    {
                        fishPower += fish.fishPower;
                        Debug.Log(fishPower);
                        yield return new WaitForSeconds(0.01f);
                    }
                    else if (fishRb.velocity.y < 0f)
                    {
                        fishPower += 0.5f;
                        yield return new WaitForSeconds(0.1f);
                    }
                    else
                    {
                        fishPower -= 0.5f;
                        yield return new WaitForSeconds(0.1f);
                    }
                }

                else if (fish.transform.position.x < transform.position.x)
                {
                    Debug.Log("ikan di kiri");
                    if (fishRb.velocity.x < 0f || fishRb.velocity.y < 0f)
                    {
                        fishPower += fish.fishPower;
                        Debug.Log(fishPower);
                        yield return new WaitForSeconds(0.01f);
                    }
                    else if (fishRb.velocity.y < 0f)
                    {
                        fishPower += 0.5f;
                        yield return new WaitForSeconds(0.1f);
                    }
                    else
                    {
                        fishPower -= 0.5f;
                        yield return new WaitForSeconds(0.1f);
                    }
                }

                yield return new WaitForSeconds(0.1f);
            }
        }

        private IEnumerator MoodChanger()
        {
            fisher.ChangeMood(10f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
