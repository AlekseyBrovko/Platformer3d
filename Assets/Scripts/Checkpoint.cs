using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject checkpointOn, checkpointOff;
    public int soundToPlay = 4;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.instance.SetSpawnPoint(transform.position);

            Checkpoint[] allCheckpoints = FindObjectsOfType<Checkpoint>();
            for(int i = 0; i < allCheckpoints.Length; i++)
            {
                allCheckpoints[i].checkpointOff.SetActive(true);
                allCheckpoints[i].checkpointOn.SetActive(false);
            }
            checkpointOff.SetActive(false);
            checkpointOn.SetActive(true);
            AudioManager.instance.PlaySFX(soundToPlay);
        }    
    }
}
