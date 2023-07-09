using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK
{
    public class Thrower : MonoBehaviour
    {
        public static Thrower instance;
        [SerializeField] private float throwForce = 10f;
        [SerializeField] private GameObject[] trashPrefabs;
        [SerializeField] private Transform throwPoint;
        [SerializeField] Animator animator;
        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DoThrow();
            }
        }

        public void DoThrow()
        {
            if (Trash.trashCount < 5)
                animator.SetTrigger("lempar");
        }

        public void ThrowTrash()
        {
            int randomIndex = Random.Range(0, trashPrefabs.Length);
            GameObject trash = Instantiate(trashPrefabs[randomIndex], transform.position, Quaternion.identity);
            Vector2 direction = (throwPoint.position - transform.position).normalized;
            float throwPower = Random.Range(5f, 20f);
            trash.GetComponent<Rigidbody2D>().velocity = direction * throwPower;
        }
    }
}
