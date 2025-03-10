using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject turretBullet;
    [SerializeField] Transform turretHead;
    [SerializeField] Transform playerTargetPoint;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float fireRate = 2f;
    [SerializeField] int damage = 3;

    private PlayerHealth playerHealth;

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
