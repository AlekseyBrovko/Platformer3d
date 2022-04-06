using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeathManager : MonoBehaviour
{
    public float maxHealth = 1;
    private float currentHealth;

    public int deathSound = 6;

    public GameObject deathEffect, itemToDrop;

    void Start()
    {
        currentHealth = maxHealth;
    }

    
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        currentHealth--;
        if(currentHealth <= 0)
        {
            AudioManager.instance.PlaySFX(deathSound);
            Destroy(gameObject);

            PlayerController.instance.Bounce();

            Instantiate(deathEffect, transform.position + new Vector3 (0, 1, 0), transform.rotation);
            Instantiate(itemToDrop, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
        }
    }
}
