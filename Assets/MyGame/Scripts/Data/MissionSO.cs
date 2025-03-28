using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionSO", menuName = "Scriptable Objects/MissionSO")]
public class MissionSO : ScriptableObject
{
    public string MissionName;
    public bool IsComplete;
    public int TotalEnemy;
    public float SpawnTime;
}
