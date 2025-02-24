using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class MyWeapon : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] private int damageAmount = 1;
    [SerializeField] private ParticleSystem muzzleFlashFx;
    private StarterAssetsInputs starterAssetsInput;

    private const string SHOOT_STATE = "Shoot";

    private void Awake()
    {
        starterAssetsInput = GetComponentInParent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleShoot();
    }

    void HandleShoot()
    {
        if (!starterAssetsInput.shoot) return;

        muzzleFlashFx?.Play();
        animator.Play(SHOOT_STATE, 0, 0f);
        starterAssetsInput.ShootInput(false);

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            //Debug.Log($"Raycast hit: {hit.collider.name}");
            MyEnemyHealth enemyHealth = hit.collider.GetComponent<MyEnemyHealth>();
            enemyHealth?.TakeDamage(damageAmount);
        }
    }
}
