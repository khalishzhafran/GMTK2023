using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace GMTK.Cameras
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
                CameraSwitcher.SetSecondCamera(virtualCamera);
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
        Default,
        Second
    }
}
