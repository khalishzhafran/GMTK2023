using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace GMTK.Cameras
{
    public static class CameraSwitcher
    {
        private static List<CinemachineVirtualCamera> virtualCameraList = new List<CinemachineVirtualCamera>();
        private static CinemachineVirtualCamera defaultCamera;
        private static CinemachineVirtualCamera secondCamera;
        private static CinemachineVirtualCamera currentCamera;

        public static void SetDefaultCamera(CinemachineVirtualCamera camera)
        {
            defaultCamera = camera;
            currentCamera = camera;
        }

        public static void SetSecondCamera(CinemachineVirtualCamera camera)
        {
            secondCamera = camera;
        }

        public static void SwitchCamera()
        {
            if (currentCamera == defaultCamera)
            {
                SetCurrentCamera(secondCamera);
            }
            else
            {
                SetCurrentCamera(defaultCamera);
            }
        }

        private static void SetCurrentCamera(CinemachineVirtualCamera newCamera)
        {
            currentCamera.Priority = 0;
            currentCamera = newCamera;
            currentCamera.Priority = 10;
        }

        public static bool IsCurrentCamera(CinemachineVirtualCamera camera)
        {
            return currentCamera == camera;
        }

        public static void Register(CinemachineVirtualCamera camera)
        {
            virtualCameraList.Add(camera);
        }

        public static void Unregister(CinemachineVirtualCamera camera)
        {
            virtualCameraList.Remove(camera);
        }
    }
}
