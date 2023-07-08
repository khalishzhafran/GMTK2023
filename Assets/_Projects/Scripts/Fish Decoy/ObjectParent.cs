using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK
{
    public class ObjectParent : MonoBehaviour
    {
        [SerializeField] Rigidbody2D rb;
        [SerializeField] Rigidbody2D rb1;
        [SerializeField] Rigidbody2D rb2;

        // Update is called once per frame
        void Update()
        {
            Vector2 directionToGo = rb1.velocity - rb2.velocity;
            rb.velocity = directionToGo;
        }
    }
}
