using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject turretBullet;
    [SerializeField] private Transform turretHead;
    [SerializeField] private Transform playerTargetPoint;
    [SerializeField] private Transform bulletSpawnPoint;
    private float fireRate;
    private int damage;

    private PlayerHealth playerHealth;

    private void Awake()
    {
        if (DataManager.HasInstance)
        {
            fireRate = DataManager.Instance.GetTurretFireRate();
            damage = DataManager.Instance.GetTurretDamage();
        }
    }

    void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        StartCoroutine(IEFire());
    }

    void Update()
    {
        if(playerTargetPoint != null)
        {
            turretHead.LookAt(playerTargetPoint);
        }
    }

    private IEnumerator IEFire()
    {
        while(playerHealth != null)
        {
            yield return new WaitForSeconds(fireRate);
            Bullet bullet = Instantiate(turretBullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation).GetComponent<Bullet>();
            bullet.transform.LookAt(playerTargetPoint);
            bullet.Init(damage);
        }
    }
}
