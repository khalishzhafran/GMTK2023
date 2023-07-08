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
        public ObjectTest asdsa;
        void Start()
        {
            fisher = FindObjectOfType<Fisher.Fisher>();
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            transform.position = transform.parent.position;
            Moving();
        }

        private void Moving()
        {
            float speedminus = speed - asdsa.multiplier;
            Vector2 horizontal = Input.GetAxis("Horizontal") * Vector2.right * speed;
            Vector2 vertical = Input.GetAxis("Vertical") * Vector2.up * speed;

            rb.velocity = horizontal + vertical;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Hook")
            {
                fisher.ChangeMood(10f);
                Debug.Log("Fish Caught");

                other.gameObject.GetComponent<Reeling>().isReeling = true;
                transform.parent = other.gameObject.transform;
                transform.position = transform.parent.position;

                isCaught = true;
            }
        }
    }
}
