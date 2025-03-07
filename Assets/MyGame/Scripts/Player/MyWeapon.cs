using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using Cinemachine;

public class MyWeapon : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleFlashFx;
    [SerializeField] float maxDistance = 500f;
    [SerializeField] LayerMask interactLayer;

    private CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shoot(WeaponSO weaponSO)
    {
        RaycastHit hit;
        muzzleFlashFx.Play();
        impulseSource.GenerateImpulse();
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance, interactLayer, QueryTriggerInteraction.Ignore))
        {
            //Debug.Log($"Raycast hit: {hit.collider.name}");
            Instantiate(weaponSO.HitVFXPrefab, hit.point, Quaternion.identity);
            MyEnemyHealth enemyHealth = hit.collider.GetComponent<MyEnemyHealth>();
            enemyHealth?.TakeDamage(weaponSO.Damage);
        }
    }
}
