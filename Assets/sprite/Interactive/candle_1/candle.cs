using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mtscoptor.Interfaces;

public class Candle : MonoBehaviour,IDamageable
{
    public GameObject[] lootItems; // 掉落物品的集合（金币、魔力恢复道具等）
    public GameObject fixedLootItem; // 固定掉落物品（如果有）
    public int health = 1; // 蜡烛的生命值（可以根据需要增加）
    [SerializeField] private GameObject hitParticles;

    private Animator animator; // 动画控制器

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();// 获取Animator
    }
    public void Damage(float amount)
    {
        Debug.Log($"{amount} Damage taken");

        Instantiate(hitParticles, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        animator.SetTrigger("damage");
        //Destroy(gameObject);
        DestroyCandle();
    }

    // 玩家攻击时调用
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            DestroyCandle();
        }
    }

    // 蜡烛破坏后的处理
    private void DestroyCandle()
    {
        // 播放破坏动画
        if (animator != null)
        {
            animator.SetBool("is_destroyed", true);
        }

        // 调用掉落物品的函数
        DropLoot();

        // 销毁蜡烛对象（可以根据需要延时销毁）
        Destroy(gameObject, 1f); // 等待动画播放完再销毁
    }

    // 掉落物品的函数
    private void DropLoot()
    {
        Vector3 dropPosition = animator.transform.position;
        // 固定掉落物品（如果有）
        if (fixedLootItem != null)
        {
            Instantiate(fixedLootItem, dropPosition, Quaternion.identity);
        }

        // 随机掉落物品（金币、魔力恢复等）
        if (lootItems.Length > 0)
        {
            int randomIndex = Random.Range(0, lootItems.Length);
            Instantiate(lootItems[randomIndex], dropPosition, Quaternion.identity);
        }
    }
}