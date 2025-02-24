using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEnemyHealth : MonoBehaviour
{
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
            Destroy(this.gameObject);
        }
    }
}
