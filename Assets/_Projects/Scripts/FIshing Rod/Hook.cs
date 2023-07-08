using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK {
    public class Hook : MonoBehaviour {
        private bool isSelected = false;
        private bool inWater = false;
        private Rigidbody2D rb;

        private void Start() {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnMouseDown() {
            isSelected = true;
        }

        private void OnMouseDrag() {
            if (isSelected) {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                transform.position = mousePos;
            }
        }

        private void OnMouseUp() {
            if (inWater) {
                isSelected = false;
                PositionBoundaries();
            }


            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
            Debug.Log(transform.position);
            foreach (Collider2D collider in colliders) {
                Debug.Log(collider.gameObject.name);
                FishingObject fishingObject = collider.gameObject.GetComponent<FishingObject>();

                if (fishingObject != null) {
                    fishingObject.Catch();
                    transform.parent.GetComponentInParent<Fisher.Fisher>().ChangeMood(fishingObject.Data.value);
                }
            }
        }



        private void PositionBoundaries() {
            if (transform.position.x < -8f) {
                transform.position = new Vector3(-8f, transform.position.y, transform.position.z);
            } else if (transform.position.x > 8.5f) {
                transform.position = new Vector3(8.5f, transform.position.y, transform.position.z);
            }
            if (transform.position.y < -4.5f) {
                transform.position = new Vector3(transform.position.x, -4.5f, transform.position.z);
            } else if (transform.position.y > 2f) {
                transform.position = new Vector3(transform.position.x, 2f, transform.position.z);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.tag == "Water") {
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
                inWater = true;
            }
        }
    }
}
