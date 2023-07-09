using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GMTK.Fisherman;
using GMTK.Cameras;
using GMTK.EventSystem;

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
        private Trash trash;
        private Hook hook;
        private Fisher fisher;
        private float fishPower;

        private void Awake()
        {
            startingPos = transform.position;
            hook = GetComponent<Hook>();
            rb = GetComponent<Rigidbody2D>();
            fisher = FindObjectOfType<Fisher>();

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
                Vector3 direction = (startingPos - transform.position).normalized;
                Vector2 hookPower = direction * reelSpeed;
                rb.velocity = hookPower + fishRb.velocity;
                hook.enabled = false;

                if (Vector2.Distance(transform.position, startingPos) < 0.1f)
                {
                    isReeling = false;
                    CameraSwitcher.SwitchCamera();

                    OnFinishFishingGame evt = Events.OnFinishFishingGame;
                    evt.isTrash = false;
                    evt.isSuccessful = true;
                    EventManager.Broadcast(evt);

                    StopAllCoroutines();

                    hook.enabled = true;
                    rb.velocity = Vector2.zero;
                    GetComponent<CircleCollider2D>().enabled = true;
                    rb.gravityScale = 3;

                    Destroy(fishRb.gameObject, 0.1f);
                    fishRb = null;
                }
            }
            else if (isReeling && trash != null)
            {
                Vector3 direction = (startingPos - transform.position).normalized;
                Vector2 hookPower = direction * reelSpeed;
                rb.velocity = hookPower;
                hook.enabled = false;

                if (Vector2.Distance(transform.position, startingPos) < 0.1f)
                {
                    isReeling = false;
                    CameraSwitcher.SwitchCamera();

                    OnFinishFishingGame evt = Events.OnFinishFishingGame;
                    evt.isTrash = true;
                    evt.isSuccessful = true;
                    EventManager.Broadcast(evt);

                    StopAllCoroutines();

                    hook.enabled = true;
                    rb.velocity = Vector2.zero;
                    GetComponent<CircleCollider2D>().enabled = true;
                    rb.gravityScale = 3;

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
        }

        public void GetTrash(Trash trash)
        {
            isReeling = true;
            this.trash = trash;

            rb.gravityScale = 0;
            GetComponent<CircleCollider2D>().enabled = false;
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
                if (fishPower > 50)
                {
                    CameraSwitcher.SwitchCamera();

                    OnFinishFishingGame evt = Events.OnFinishFishingGame;
                    evt.isSuccessful = false;
                    EventManager.Broadcast(evt);

                    fish.isCaught = false;
                    if (fish.transform.position.x > transform.position.x) fishRb.velocity = Vector2.right * 5;
                    else fishRb.velocity = Vector2.left * 5;

                    Destroy(fishRb.gameObject, 3f);

                    fishRb = null;
                    fish = null;

                    StopAllCoroutines();

                    fishPower = 0;
                }
                if (fish != null)
                {
                    OnReeling evt = Events.OnReeling;
                    evt.fishPower = fishPower;
                    evt.maxFishPower = 50;
                    EventManager.Broadcast(evt);

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
                            fishPower -= 1;
                            yield return new WaitForSeconds(0.1f);
                        }
                    }

                    else if (fish.transform.position.x < transform.position.x)
                    {
                        if (fishRb.velocity.x < 0f || fishRb.velocity.y < 0f)
                        {
                            fishPower += fish.fishPower;
                            yield return new WaitForSeconds(0.01f);
                        }
                        else
                        {
                            fishPower -= 1;
                            yield return new WaitForSeconds(0.1f);
                        }
                    }
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
