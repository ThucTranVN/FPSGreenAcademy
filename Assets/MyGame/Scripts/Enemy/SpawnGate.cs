using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGate : MonoBehaviour
{
    [SerializeField] private GameObject robotPrefab;
    [SerializeField] private Transform spawnPoint;

    private float spawnTime;
    private int maxSpawnAmount;
    private int maxSpawnCount = 0;

    private PlayerHealth playerHealth;
    [SerializeField]
    private MissionSO missionSO;

    private void Awake()
    {
        if (MissionManager.HasInstance)
        {
            missionSO = MissionManager.Instance.CurrentMission;
            spawnTime = missionSO.SpawnTime;
            maxSpawnAmount = missionSO.TotalEnemy;
        }
    }

    private void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        StartCoroutine(IESpawnRobot());

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.UPDATE_MISSION, OnUpdateMission);
        }
    }

    private void OnDestroy()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Unregister(ListenType.UPDATE_MISSION, OnUpdateMission);
        }
    }

    private IEnumerator IESpawnRobot()
    {
        while (playerHealth && maxSpawnCount < maxSpawnAmount)
        {
            maxSpawnCount++;
            Instantiate(robotPrefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(spawnTime);
        }

    }

    private void OnUpdateMission(object value)
    {
        if(value != null)
        {
            if(value is MissionSO updateMission)
            {
                missionSO = updateMission;
                spawnTime = missionSO.SpawnTime;
                maxSpawnAmount = missionSO.TotalEnemy;
                maxSpawnCount = 0;
                StartCoroutine(IESpawnRobot());
            }
        }
    }

}
