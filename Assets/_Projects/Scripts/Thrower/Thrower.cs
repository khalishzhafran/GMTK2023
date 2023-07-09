using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK
{
    public class Thrower : MonoBehaviour
    {
        [SerializeField] private float throwForce = 10f;
        [SerializeField] private GameObject[] trashPrefabs;
        [SerializeField] private Transform throwPoint;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ThrowTrash();
            }
        }

        public void ThrowTrash()
        {
            int randomIndex = Random.Range(0, trashPrefabs.Length);
            GameObject trash = Instantiate(trashPrefabs[randomIndex], transform.position, Quaternion.identity);
            Vector2 direction = (throwPoint.position - transform.position).normalized;
            trash.GetComponent<Rigidbody2D>().velocity = direction * throwForce;
        }
    }
}
