using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class MyWeapon : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleFlashFx;

    public void Shoot(WeaponSO weaponSO)
    {
        RaycastHit hit;
        muzzleFlashFx.Play();
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            //Debug.Log($"Raycast hit: {hit.collider.name}");
            Instantiate(weaponSO.HitVFXPrefab, hit.point, Quaternion.identity);
            MyEnemyHealth enemyHealth = hit.collider.GetComponent<MyEnemyHealth>();
            enemyHealth?.TakeDamage(weaponSO.Damage);
        }
    }
}
