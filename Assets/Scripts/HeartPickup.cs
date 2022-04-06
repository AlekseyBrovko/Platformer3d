using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    public int healAmount;
    public bool isFullHeal;
    public GameObject heartPickupEffect;
    public int soundToPlay = 7;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(soundToPlay);

            Instantiate(heartPickupEffect, transform.position, transform.rotation);
            if (isFullHeal)
            {
                HealthManager.instance.ResetHealth();
            }
            else
            {
                HealthManager.instance.AddHealth(healAmount);
            }
        }
    }
}
