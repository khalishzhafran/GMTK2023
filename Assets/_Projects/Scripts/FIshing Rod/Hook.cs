using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK
{
    public class Hook : MonoBehaviour
    {
        [SerializeField] AudioClip splashClip;
        [SerializeField] AudioClip dragClip;
        [SerializeField] private CircleCollider2D wall;
        [SerializeField] private CircleCollider2D trigger;
        private bool isSelected = false;
        private bool inWater = false;
        private Rigidbody2D rb;
        private Hook hook;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            hook = GetComponent<Hook>();
        }

        public void ColliderCOntrol(bool state)
        {
            wall.enabled = state;
            trigger.enabled = state;
        }

        private void OnMouseDown()
        {
            isSelected = true;

            SoundManager.instance.PlaySFX(SoundManager.instance.soundEffectSO.HookClicked);

            ColliderCOntrol(false);
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

                SoundManager.instance.PlaySFX(SoundManager.instance.soundEffectSO.HookDrop);

                isSelected = false;
                PositionBoundaries();

                ColliderCOntrol(true);
            }
        }

        private void PositionBoundaries()
        {
            if (transform.position.x < -12f)
            {
                transform.position = new Vector3(-12f, transform.position.y, transform.position.z);
            }
            else if (transform.position.x > 12f)
            {
                transform.position = new Vector3(12f, transform.position.y, transform.position.z);
            }
            if (transform.position.y < -7f)
            {
                transform.position = new Vector3(transform.position.x, -7f, transform.position.z);
            }
            else if (transform.position.y > -2f)
            {
                transform.position = new Vector3(transform.position.x, -2f, transform.position.z);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Water")
            {
                SoundManager.instance.PlaySFX(SoundManager.instance.soundEffectSO.HookDrop);
                rb.gravityScale = 0.5f;
                rb.velocity = Vector2.zero;
                inWater = true;
            }
        }
    }
}
