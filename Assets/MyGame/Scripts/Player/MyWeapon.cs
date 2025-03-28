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
    [SerializeField] private Transform firePoint;

    private CinemachineImpulseSource impulseSource;

    private Vector3 hitPoint;

    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shoot(WeaponSO weaponSO)
    {
        RaycastHit hit;
        muzzleFlashFx.Play();
        impulseSource.GenerateImpulse();
        PlayShootSE(weaponSO);
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, maxDistance, interactLayer, QueryTriggerInteraction.Ignore))
        {
            //Debug.Log($"Raycast hit: {hit.collider.name}");
            Instantiate(weaponSO.HitVFXPrefab, hit.point, Quaternion.identity);
            MyEnemyHealth enemyHealth = hit.collider.GetComponent<MyEnemyHealth>();
            enemyHealth?.TakeDamage(weaponSO.Damage);
        }
    }

    private void Update()
    {
        Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit hitTest, maxDistance, interactLayer, QueryTriggerInteraction.Ignore);
        hitPoint = hitTest.point;
    }

    private void PlayShootSE(WeaponSO weaponSO)
    {
        if (AudioManager.HasInstance)
        {
            switch (weaponSO.weaponType)
            {
                case WeaponType.Pistol:
                    AudioManager.Instance.PlaySE(AUDIO.SE_PISTOLSHOT);
                    break;
                case WeaponType.Riffle:
                    AudioManager.Instance.PlaySE(AUDIO.SE_RIFLESHOT);
                    break;
                case WeaponType.Sniper:
                    AudioManager.Instance.PlaySE(AUDIO.SE_SNIPERSHOT);
                    break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(hitPoint, 1);
    }
}
