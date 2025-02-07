using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicRestorePickup : MonoBehaviour
{
    public ManaBarController manabarUI;
    public float restoreAmount = 1f; // 恢复魔力的数量
    public float pickupRadius = 1.5f; // 玩家接近时可拾取的范围
    private void Start()
    {
        // 查找场景中第一个ManaBarController对象
        manabarUI = FindObjectOfType<ManaBarController>();
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         Player player = other.GetComponent<Player>();
    //         if (player != null)
    //         {
    //             player.RestoreMagic(restoreAmount);
    //             manabarUI.RestoreMana(restoreAmount);
    //             Destroy(gameObject);
    //         }
    //     }
    // }
}
