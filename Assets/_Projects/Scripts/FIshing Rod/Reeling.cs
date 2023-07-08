using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK
{
    public class Reeling : MonoBehaviour
    {
        [SerializeField] private float reelSpeed = 1f;
        public bool isReeling { get; set; } = false;
        Vector3 startingPos;

        private Rigidbody2D rb;
        private Hook hook;

        private void Awake()
        {
            startingPos = transform.position;
            hook = GetComponent<Hook>();
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            ReelingIn();
        }

        private void ReelingIn()
        {
            if (isReeling)
            {
                Debug.Log("Reeling");
                Vector3 direction = (startingPos - transform.position).normalized;
                rb.velocity = direction * reelSpeed;
                hook.enabled = false;

                if (Vector2.Distance(transform.position, startingPos) < 0.1f)
                {
                    hook.enabled = true;
                    isReeling = false;
                    rb.velocity = Vector2.zero;
                }

            }
        }
    }
}
