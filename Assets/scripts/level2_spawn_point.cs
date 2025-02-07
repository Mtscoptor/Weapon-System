using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        
        if (player != null)
        {
            // 设置玩家位置为当前空对象的位置
            player.transform.position = transform.position;

            // 检查玩家的控制脚本是否被禁用，若禁用则启用
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null && !playerController.enabled)
            {
                playerController.enabled = true;
            }
        }
    }
}
