using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public PlayerStats playerStats;

    // Start is called before the first frame update
   
   void Start()
    {
        if (playerStats==null)
        {
            playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        }
        
    }    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = PlayerManager.instance.player;
        if (other.CompareTag("Player"))
        {
            playerStats.TakeDamage(99f);           
            player.Damaged(-player.facingDir);
            player.stateMachine.ChangeState(player.attackedState);            
        }
    }
}
