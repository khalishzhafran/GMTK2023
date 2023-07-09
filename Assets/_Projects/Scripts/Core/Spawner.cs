using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK.Core
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> spawnObjectList;

        [SerializeField] private float spawnRate;

        private void Start()
        {
            InvokeRepeating("SpawnObject", 0f, spawnRate);
        }

        private void SpawnObject()
        {
            int randomIndex = Random.Range(0, spawnObjectList.Count);
            Instantiate(spawnObjectList[randomIndex], transform.position, Quaternion.identity);
        }
    }
}
