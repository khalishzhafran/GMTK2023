using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK
{
    public class CokGateli : MonoBehaviour
    {
        public void Throw()
        {
            Thrower.instance.ThrowTrash();
        }
    }
}
