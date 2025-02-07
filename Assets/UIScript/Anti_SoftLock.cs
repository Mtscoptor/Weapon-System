using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiSoftLock : MonoBehaviour
{
    public PlayerStats playerStats;
    public move_up1 platformScript;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (platformScript.isMoved)
            {
                playerStats.isStuck = true;
            }
        }
    }

    void update()
    {
        if(!platformScript.isMoved && playerStats.isStuck)
        {
            playerStats.isStuck = false;
        }
    }
    
}
