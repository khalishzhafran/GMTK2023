using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK
{
    public class Hook : MonoBehaviour
    {
        private bool isSelected = false;
        private bool inWater = false;
        private Rigidbody2D rb;
        private CircleCollider2D col;
        private Hook hook;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            col = GetComponent<CircleCollider2D>();
            hook = GetComponent<Hook>();
        }

        private void OnMouseDown()
        {
            isSelected = true;
            col.enabled = false;
            Debug.Log("Mouse Down");
        }

        private void OnMouseDrag()
        {
            if (isSelected && hook.enabled)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                transform.position = mousePos;
            }
        }

        private void OnMouseUp()
        {
            if (inWater)
            {

                isSelected = false;
                PositionBoundaries();

                col.enabled = true;
            }
        }

        private void PositionBoundaries()
        {
            if (transform.position.x < -8f)
            {
                transform.position = new Vector3(-8f, transform.position.y, transform.position.z);
            }
            else if (transform.position.x > 8.5f)
            {
                transform.position = new Vector3(8.5f, transform.position.y, transform.position.z);
            }
            if (transform.position.y < -4.5f)
            {
                transform.position = new Vector3(transform.position.x, -4.5f, transform.position.z);
            }
            else if (transform.position.y > 2f)
            {
                transform.position = new Vector3(transform.position.x, 2f, transform.position.z);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Water")
            {
                rb.gravityScale = 0.5f;
                rb.velocity = Vector2.zero;
                inWater = true;
            }
        }
    }
}
