using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

using GMTK.Cameras;
using GMTK.EventSystem;

namespace GMTK
{
    public class Testing : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera defaultCamera;
        [SerializeField] private CinemachineVirtualCamera secondCamera;

        [SerializeField] private List<FishTest> hookedObjects;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (CameraSwitcher.IsCurrentCamera(defaultCamera))
                {
                    CameraSwitcher.SwitchCamera();

                    int randomObject = Random.Range(0, hookedObjects.Count);

                    OnHookedObject evt = Events.OnHookedObject;
                    evt.hookedObject = hookedObjects[randomObject].transform;
                    evt.barIncreasedSpeedPerSecond = hookedObjects[randomObject].barIncreasedSpeedPerSecond;
                    evt.maxGain = hookedObjects[randomObject].maxGain;
                    EventManager.Broadcast(evt);
                }
                else
                {
                    CameraSwitcher.SwitchCamera();

                    OnFinishFishingGame evt = Events.OnFinishFishingGame;
                    EventManager.Broadcast(evt);
                }
            }
        }
    }
}
