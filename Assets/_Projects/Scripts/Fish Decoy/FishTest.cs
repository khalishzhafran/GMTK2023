using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK
{
    public class FishTest : MonoBehaviour
    {
        private Fisher.Fisher fisher;
        private bool isCaught = false;
        private Rigidbody2D rb;
        public float speed = 1f;
        public float fishPower = 1;
        void Awake()
        {
            fisher = FindObjectOfType<Fisher.Fisher>();
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
            if (other.gameObject.tag == "Hook")
            {
                isCaught = true;

                fisher.ChangeMood(10f);

                other.gameObject.GetComponent<Reeling>().GetFish(rb);


                transform.parent = other.gameObject.transform;
                transform.position = transform.parent.position;

            }
        }
    }
}
