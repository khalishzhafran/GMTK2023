using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK
{
    public class FishingLine : MonoBehaviour
    {
        [SerializeField] private GameObject hook;
        private LineRenderer lineRenderer;

        private void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            Vector3 offsetMousePos = hook.transform.position - transform.position;
            lineRenderer.SetPosition(1, offsetMousePos);
        }
    }
}
