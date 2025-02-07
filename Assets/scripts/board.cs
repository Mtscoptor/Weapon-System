using UnityEngine;

public class board : MonoBehaviour
{
    public int fallThreshold = 3;   // 触发木板落下的次数
    private int fallCount = 0;      // 累计角色落到木板上的次数
    private bool hasFallen = false; // 标记木板是否已经落下

    private Rigidbody2D rb;

    void Start()
    {
        // 获取木板的 Rigidbody2D，并确保它初始为 Kinematic
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查是否是角色碰到木板，并且木板还未落下
        if (collision.gameObject.CompareTag("Player") && !hasFallen)
        {
            // 检查角色是否从上方碰到木板
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.point.y > transform.position.y)
                {
                    fallCount++; // 增加落到木板上的次数
                    Debug.Log("角色落到木板上次数: " + fallCount);

                    // 当累计次数达到阈值时，触发木板自由落下
                    if (fallCount >= fallThreshold)
                    {
                        rb.bodyType = RigidbodyType2D.Dynamic; // 切换为 Dynamic，使木板自由落下
                        hasFallen = true; // 标记木板已落下
                    }
                    break; // 找到有效的接触点后退出循环
                }
            }
        }
    }
}
