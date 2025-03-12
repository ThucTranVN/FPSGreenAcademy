using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalConfigSO", menuName = "Scriptable Objects/GlobalConfigSO")]
public class GlobalConfigSO : ScriptableObject
{
    [Header("Player")]
    public int StartingHealth;

    [Header("UI")]
    public float LoadingTime;
    public float FadeTime;

    [Header("Camera")]
    public int DeathCameraPriority;

    [Header("Enemy")]
    public float ExplosionRadius = 1.5f;
    public int ExplosionDamage = 3;
    public float EnemySpawnTime = 5f;
    public float TurretFireRate = 2f;
    public int TurretDamage = 3;
    public float TurretBulletSpeed = 15f;
    public int RobotStartingHealth = 3;
}
