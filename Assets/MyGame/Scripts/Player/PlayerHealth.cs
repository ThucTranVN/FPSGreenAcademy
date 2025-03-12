using UnityEngine;
using Cinemachine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera deathVirtualCamera;
    [SerializeField] Transform weaponCamera;
    private int deathCameraPriority;

    private int currentHealth;

    private void Awake()
    {
        if (DataManager.HasInstance)
        {
            currentHealth = DataManager.Instance.GetStartingHealth();
            deathCameraPriority = DataManager.Instance.GetDeathCameraPriority();
        }
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
            Destroy(this.gameObject);
        }
    }
}
