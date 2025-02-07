using UnityEngine;

public class TestCandle : MonoBehaviour
{
    public Candle candle; // 引用蜡烛对象

    private void Update()
    {
        // 通过测试直接破坏蜡烛
        if (Input.GetKeyDown(KeyCode.B))
        {
            TestDestroyCandle();
        }
        
    }

    // 测试蜡烛被摧毁
    private void TestDestroyCandle()
    {
        // 模拟攻击，给蜡烛造成伤害
        if (candle != null)
        {
            candle.TakeDamage(1);  // 造成 1 点伤害，摧毁蜡烛
        }
    }
}

