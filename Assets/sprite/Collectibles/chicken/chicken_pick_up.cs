using UnityEngine;

public class ChickenPickUp : MonoBehaviour
{
    public float Heal; // 
    private PlayerStats playerStats; // 缓存 GoldAmountDisplay 的引用

    private void Start()
    {
        // 在场景中查找 GoldAmountDisplay 并缓存引用
        playerStats = FindObjectOfType<PlayerStats>();
        Heal = (playerStats.maxHealth/2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检测碰撞到的对象是否是玩家
        if (collision.CompareTag("Player") && playerStats != null)
        {
            playerStats.RestoreHealth(Heal); // 增加金币数量
            Destroy(gameObject); // 销毁金币对象
        }
    }
}
