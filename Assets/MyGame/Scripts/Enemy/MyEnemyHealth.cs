using UnityEngine;

public class MyEnemyHealth : MonoBehaviour
{
    [SerializeField] private Transform vfxPos;
    [SerializeField] private GameObject robotExplosionVFX;
    private int startingHealth;

    private int currentHealth;

    private void Awake()
    {
        if (DataManager.HasInstance)
        {
            startingHealth = DataManager.Instance.GetRobotStartingHealth();
            currentHealth = startingHealth;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            SelfDestroy();
        }
    }

    public void SelfDestroy()
    {
        Instantiate(robotExplosionVFX, vfxPos.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
