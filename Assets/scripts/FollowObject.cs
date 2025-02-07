using UnityEngine;

public class FollowObject : MonoBehaviour
{
    // 在编辑器中可以设置的公共对象
    public GameObject targetObject;

    // 在每次物理帧更新时执行
    void FixedUpdate()
    {
        if (targetObject != null)
        {
            // 设置当前物体的位置为目标物体的位置
            transform.position = targetObject.transform.position;
        }
    }

    // 检测碰撞时执行
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 如果碰撞到的对象有玩家标签
        if (collision.gameObject.CompareTag("Player"))
        {
            // 将玩家设置为自己的子对象
            collision.transform.SetParent(transform);
        }
    }

    // 检测离开碰撞时执行
    private void OnCollisionExit2D(Collision2D collision)
    {
        // 如果离开的对象有玩家标签
        if (collision.gameObject.CompareTag("Player"))
        {
            // 取消玩家作为子对象
            collision.transform.SetParent(null);
        }
    }
}
