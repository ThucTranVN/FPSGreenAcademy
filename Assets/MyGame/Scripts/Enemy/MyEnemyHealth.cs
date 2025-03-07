using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemyHealth : MonoBehaviour
{
    [SerializeField] private Transform vfxPos;
    [SerializeField] private GameObject robotExplosionVFX;
    [SerializeField] private int startingHealth = 3;

    private int currentHealth;

    private void Awake()
    {
        currentHealth = startingHealth;
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
