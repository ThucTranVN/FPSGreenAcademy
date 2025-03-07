using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGate : MonoBehaviour
{
    [SerializeField] private GameObject robotPrefab;
    [SerializeField] private float spawnTime = 5f;
    [SerializeField] private Transform spawnPoint;

    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        StartCoroutine(IESpawnRobot());
    }

    private IEnumerator IESpawnRobot()
    {
        while (playerHealth)
        {
            Instantiate(robotPrefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(spawnTime);
        }

    }

}
