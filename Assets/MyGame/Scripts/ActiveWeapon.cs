using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] Animator animator;
    private StarterAssetsInputs starterAssetsInput;
    private MyWeapon currentWeapon;
    private const string SHOOT_STATE = "Shoot";
    private float timeSinceLastShoot = 0f;

    private void Awake()
    {
        starterAssetsInput = GetComponentInParent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentWeapon = GetComponentInChildren<MyWeapon>();
    }

    private void Update()
    {
        timeSinceLastShoot += Time.deltaTime;
        HandleShoot();
    }

    private void HandleShoot()
    {
        if (!starterAssetsInput.shoot) return;

        if(timeSinceLastShoot >= weaponSO.FireRate)
        {
            animator.Play(SHOOT_STATE, 0, 0f);
            currentWeapon.Shoot(weaponSO);
            timeSinceLastShoot = 0f;
        }

        starterAssetsInput.ShootInput(false);
    }
}
