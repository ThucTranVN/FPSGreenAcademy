using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using Cinemachine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] GameObject zoomUI;
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] Animator animator;
    private FirstPersonController firstPersonController;
    private StarterAssetsInputs starterAssetsInput;
    private MyWeapon currentWeapon;
    private const string SHOOT_STATE = "Shoot";
    private float timeSinceLastShoot = 0f;
    private float defaultFOV;
    private float defaultRotationSpeed;

    private void Awake()
    {
        starterAssetsInput = GetComponentInParent<StarterAssetsInputs>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
        animator = GetComponent<Animator>();
        defaultFOV = playerFollowCamera.m_Lens.FieldOfView;
        defaultRotationSpeed = firstPersonController.RotationSpeed;
    }

    private void Start()
    {
        currentWeapon = GetComponentInChildren<MyWeapon>();
    }

    private void Update()
    {
        HandleShoot();
        HandleZoom();
    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        Debug.Log("Player pickup " + weaponSO.name);

        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
        }

        GameObject goNewWeapon = Instantiate(weaponSO.weaponPrefab, this.transform);
        currentWeapon = goNewWeapon.GetComponent<MyWeapon>();
        this.weaponSO = weaponSO;
    }

    private void HandleShoot()
    {
        timeSinceLastShoot += Time.deltaTime;

        if (!starterAssetsInput.shoot) return;

        if(timeSinceLastShoot >= weaponSO.FireRate)
        {
            animator.Play(SHOOT_STATE, 0, 0f);
            currentWeapon.Shoot(weaponSO);
            timeSinceLastShoot = 0f;
        }

        if (!weaponSO.IsAutomatic)
        {
            starterAssetsInput.ShootInput(false);
        }

    }

    private void HandleZoom()
    {
        if (!weaponSO.IsCanZoom) return;

        if (starterAssetsInput.zoom)
        {
            Debug.Log("Zoom In");
            playerFollowCamera.m_Lens.FieldOfView = weaponSO.ZoomAmount;
            zoomUI.SetActive(true);
            firstPersonController.ChangeRotationSpeed(weaponSO.ZoomRotationSpeed);
        }
        else
        {
            Debug.Log("Zoom Out");
            playerFollowCamera.m_Lens.FieldOfView = defaultFOV;
            zoomUI.SetActive(false);
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
        }
    }
}
