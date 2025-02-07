using UnityEngine;

public class ManaPickUp : MonoBehaviour
{
    public int manaValue = 10; // 
    private PlayerStats playerStats; // 缓存 GoldAmountDisplay 的引用

    private void Start()
    {
        // 在场景中查找 GoldAmountDisplay 并缓存引用
        playerStats = FindObjectOfType<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检测碰撞到的对象是否是玩家
        if (collision.CompareTag("Player") && playerStats != null)
        {
            playerStats.RestoreMana(manaValue); // 增加金币数量
            Destroy(gameObject); // 销毁金币对象
        }
    }
}
