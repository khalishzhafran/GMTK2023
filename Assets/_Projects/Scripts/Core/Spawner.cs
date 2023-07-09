using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK.Core
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private List<FishTest> spawnObjectList;

        [SerializeField] private float spawnTopBound;
        [SerializeField] private float spawnBottomBound;
        [SerializeField] private float spawnRate;
        [SerializeField] private bool leftSpawner;

        [SerializeField] private bool drawGizmos;

        private void Start()
        {
            InvokeRepeating("SpawnObject", 0f, spawnRate);
        }

        private void SpawnObject()
        {
            if (FishTest.fishCount >= 5)
                return;

            int randomIndex = Random.Range(0, spawnObjectList.Count);
            float randomYPosition = Random.Range(spawnBottomBound, spawnTopBound);

            FishTest fish = Instantiate(spawnObjectList[randomIndex], new Vector2(transform.position.x, randomYPosition + transform.position.y), Quaternion.identity);

            if (leftSpawner)
            {
                fish.direction = 1;
            }
            else
            {
                fish.direction = -1;
            }
        }

        private void OnDrawGizmos()
        {
            if (drawGizmos)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, spawnTopBound + transform.position.y));
                Gizmos.color = Color.white;
                Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, spawnBottomBound + transform.position.y));
            }
        }
    }
}
