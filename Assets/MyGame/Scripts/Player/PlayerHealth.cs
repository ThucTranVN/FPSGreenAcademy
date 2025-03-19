using UnityEngine;
using Cinemachine;
using StarterAssets;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera deathVirtualCamera;
    [SerializeField] Transform weaponCamera;
    [SerializeField] StarterAssetsInputs input;
    [SerializeField] ActiveWeapon activeWeapon;

    private int deathCameraPriority;

    private int currentHealth;

    private void Awake()
    {
        if (DataManager.HasInstance)
        {
            currentHealth = DataManager.Instance.GetStartingHealth();
            deathCameraPriority = DataManager.Instance.GetDeathCameraPriority();
        }

        input = GetComponent<StarterAssetsInputs>();
    }

    private void Start()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.BroadCast(ListenType.UPDATE_HEALTH, currentHealth);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.BroadCast(ListenType.UPDATE_HEALTH, currentHealth);
        }

        if (currentHealth <= 0)
        {
            weaponCamera.parent = null;
            deathVirtualCamera.Priority = deathCameraPriority;
            input.cursorLocked = false;
            input.cursorInputForLook = false;
            activeWeapon.gameObject.SetActive(false);

            DOVirtual.DelayedCall(2f, () =>
            {
                if (GameManager.HasInstance)
                {
                    GameManager.Instance.GameOver();
                }
            });
        }
    }
}
