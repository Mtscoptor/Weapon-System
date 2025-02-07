using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinValue = 1; // 每个金币增加的数量
    private PlayerStats goldManager; // 缓存 GoldAmountDisplay 的引用

    private void Start()
    {
        // 在场景中查找 GoldAmountDisplay 并缓存引用
        goldManager = FindObjectOfType<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检测碰撞到的对象是否是玩家
        if (collision.CompareTag("Player") && goldManager != null)
        {
            goldManager.AddGold(coinValue); // 增加金币数量
            Destroy(gameObject); // 销毁金币对象
        }
    }
}
