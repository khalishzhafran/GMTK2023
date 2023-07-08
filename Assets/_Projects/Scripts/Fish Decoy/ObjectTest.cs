using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK
{
    public class ObjectTest : MonoBehaviour
    {
        public Rigidbody2D rb;
        public Rigidbody2D rb1;
        public int multiplier = 1;
        public Vector2 totalPower;

        private void Update()
        {
            totalPower = Vector2.right * multiplier;
            rb.velocity = rb1.velocity - totalPower;
        }
    }
}