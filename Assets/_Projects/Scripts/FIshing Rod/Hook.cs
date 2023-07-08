using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK
{
    public class Hook : MonoBehaviour
    {
        private bool isSelected = false;

        private void OnMouseDown()
        {
            isSelected = true;
        }

        private void OnMouseDrag()
        {
            if (isSelected)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                transform.position = mousePos;
            }
        }

        private void OnMouseUp()
        {
            isSelected = false;
        }
    }
}
