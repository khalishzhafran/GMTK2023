using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace GMTK.Camera
{
    public class VCamera : MonoBehaviour
    {
        protected CinemachineVirtualCamera virtualCamera;

        [SerializeField] protected CameraSetting cameraSetting;

        protected virtual void Awake()
        {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();

            if (cameraSetting == CameraSetting.Default)
            {
                virtualCamera.Priority = 10;
                CameraSwitcher.SetDefaultCamera(virtualCamera);
            }
            else
            {
                virtualCamera.Priority = 0;
            }
        }

        protected virtual void OnEnable()
        {
            CameraSwitcher.Register(virtualCamera);
        }

        protected virtual void OnDisable()
        {
            CameraSwitcher.Unregister(virtualCamera);
        }
    }

    public enum CameraSetting
    {
        None,
        Default
    }
}
