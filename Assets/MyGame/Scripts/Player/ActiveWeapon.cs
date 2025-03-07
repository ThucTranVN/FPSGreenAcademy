using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using Cinemachine;
using TMPro;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] Camera weaponCamera;
    [SerializeField] GameObject zoomUI;
    [SerializeField] WeaponSO startingWeaponSO;
    [SerializeField] Animator animator;
    [SerializeField] TMP_Text ammoText;

    private FirstPersonController firstPersonController;
    private StarterAssetsInputs starterAssetsInput;
    private MyWeapon currentWeapon;
    private WeaponSO currentWeaponSO;

    private const string SHOOT_STATE = "Shoot";
    private float timeSinceLastShoot = 0f;
    private float defaultFOV;
    private float defaultRotationSpeed;
    private int currentAmmo;

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
        SwitchWeapon(startingWeaponSO);
        AdjustAmmo(this.currentWeaponSO.MagazineSize);
    }

    private void Update()
    {
        HandleShoot();
        HandleZoom();
    }

    public void AdjustAmmo(int amount)
    {
        currentAmmo += amount;

        if(currentAmmo > currentWeaponSO.MagazineSize)
        {
            currentAmmo = currentWeaponSO.MagazineSize;
        }

        ammoText.text = currentAmmo.ToString("D2");
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
        this.currentWeaponSO = weaponSO;
        AdjustAmmo(this.currentWeaponSO.MagazineSize);
    }

    private void HandleShoot()
    {
        if (currentWeapon == null) return;

        timeSinceLastShoot += Time.deltaTime;

        if (!starterAssetsInput.shoot) return;

        if(timeSinceLastShoot >= currentWeaponSO.FireRate && currentAmmo > 0)
        {
            animator.Play(SHOOT_STATE, 0, 0f);
            currentWeapon.Shoot(currentWeaponSO);
            timeSinceLastShoot = 0f;
            AdjustAmmo(-1);
        }

        if (!currentWeaponSO.IsAutomatic)
        {
            starterAssetsInput.ShootInput(false);
        }

    }

    private void HandleZoom()
    {
        if (!currentWeaponSO.IsCanZoom) return;

        if (starterAssetsInput.zoom)
        {
            Debug.Log("Zoom In");
            playerFollowCamera.m_Lens.FieldOfView = currentWeaponSO.ZoomAmount;
            weaponCamera.fieldOfView = currentWeaponSO.ZoomAmount;
            zoomUI.SetActive(true);
            firstPersonController.ChangeRotationSpeed(currentWeaponSO.ZoomRotationSpeed);
        }
        else
        {
            Debug.Log("Zoom Out");
            playerFollowCamera.m_Lens.FieldOfView = defaultFOV;
            weaponCamera.fieldOfView = defaultFOV;
            zoomUI.SetActive(false);
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
        }
    }
}
