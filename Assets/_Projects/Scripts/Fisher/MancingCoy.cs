using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK
{
    public class MancingCoy : MonoBehaviour
    {
        public static MancingCoy instance;
        [SerializeField] Transform throwPoint;
        [SerializeField] GameObject hookPrefab;
        private Animator animator;

        private void Awake()
        {
            instance = this;
            animator = GetComponent<Animator>();
        }

        public void SpawnHook()
        {
            Instantiate(hookPrefab, throwPoint.position, Quaternion.identity);
        }

        public void ReelIn()
        {
            animator.SetBool("idle", false);
        }

        public void Throw()
        {
            animator.SetTrigger("throw");
            animator.SetBool("idle", true);
        }
    }
}
