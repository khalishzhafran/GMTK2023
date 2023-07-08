using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

using GMTK.Camera;
using GMTK.EventSystem;

namespace GMTK
{
    public class Testing : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera defaultCamera;
        [SerializeField] private CinemachineVirtualCamera secondCamera;

        [SerializeField] private List<Transform> hookedObjects;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (CameraSwitcher.IsCurrentCamera(defaultCamera))
                {
                    CameraSwitcher.SwitchCamera(secondCamera);

                    int randomObject = Random.Range(0, hookedObjects.Count);

                    OnHookedObject evt = Events.OnHookedObject;
                    evt.hookedObject = hookedObjects[randomObject];
                    EventManager.Broadcast(evt);
                }
                else
                {
                    CameraSwitcher.SwitchCamera(defaultCamera);

                    OnFinishFishingGame evt = Events.OnFinishFishingGame;
                    EventManager.Broadcast(evt);
                }
            }
        }
    }
}
