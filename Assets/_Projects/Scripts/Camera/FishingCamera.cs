using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GMTK.EventSystem;

namespace GMTK.Camera
{
    public class FishingCamera : VCamera
    {
        protected override void OnEnable()
        {
            base.OnEnable();

            EventManager.AddListener<OnHookedObject>(OnHookedObject);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            EventManager.RemoveListener<OnHookedObject>(OnHookedObject);
        }

        private void OnHookedObject(OnHookedObject evt)
        {
            virtualCamera.Follow = evt.hookedObject.transform;
        }
    }
}
