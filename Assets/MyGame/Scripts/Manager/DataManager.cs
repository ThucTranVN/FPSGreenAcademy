using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : BaseManager<DataManager>
{
    [SerializeField]
    private GlobalConfigSO GlobalConfigSO;

    public int GetStartingHealth()
    {
        return GlobalConfigSO.StartingHealth;
    }

    public float GetLoadingTime()
    {
        return GlobalConfigSO.LoadingTime;
    }

    public float GetFadeTime()
    {
        return GlobalConfigSO.FadeTime;
    }

    public int GetDeathCameraPriority()
    {
        return GlobalConfigSO.DeathCameraPriority;
    }

    public float GetExplosionRadius()
    {
        return GlobalConfigSO.ExplosionRadius;
    }

    public int GetExplosionDamage()
    {
        return GlobalConfigSO.ExplosionDamage;
    }

    public float GetEnemySpawnTime()
    {
        return GlobalConfigSO.EnemySpawnTime;
    }

    public float GetTurretFireRate()
    {
        return GlobalConfigSO.TurretFireRate;
    }

    public int GetTurretDamage()
    {
        return GlobalConfigSO.TurretDamage;
    }

    public float GetTurretBulletSpeed()
    {
        return GlobalConfigSO.TurretBulletSpeed;
    }

    public int GetRobotStartingHealth()
    {
        return GlobalConfigSO.RobotStartingHealth;
    }
}
