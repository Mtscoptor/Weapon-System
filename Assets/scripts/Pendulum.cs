
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public Transform pivot;            // 摆锤旋转的中心点
    public Transform pivot2;            // 摆锤中心位置
    public float amplitude = 45f;      // 振动幅度（度）
    public float period = 2f;          // 振动周期（秒）
    public float heightOffset = 1f;    // 角色的Y轴偏移量（相对于pivot2）
    private float timeElapsed = 0f;    // 经过的时间
    private Transform player;          // 角色的 Transform

    // 角色的 Rigidbody2D 用于控制旋转约束
    private Rigidbody2D playerRigidbody;

    void Update()
    {
        // 更新时间
        timeElapsed += Time.deltaTime;

        // 打印 timeElapsed 到控制台
        //Debug.Log("Time Elapsed: " + timeElapsed);

        // 计算角频率 ω = 2π / T，其中 T 是周期
        float angularFrequency = 2f * Mathf.PI / period;

        // 使用简谐振动公式 θ(t) = A * cos(ω * t)
        // 将振幅从度转换为弧度
        float currentAngle = amplitude * Mathf.Cos(angularFrequency * timeElapsed);

        // 通过旋转物体，使其围绕 pivot 旋转
        transform.RotateAround(pivot.position, Vector3.forward, currentAngle * Time.deltaTime);

        // 确保玩家始终站在摆锤的中心
        if (player != null)
        {
            // 锁定玩家的旋转
            playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

            Vector3 newPlayerPosition = new Vector3(pivot2.position.x, pivot2.position.y + heightOffset, player.position.z);
            player.position = newPlayerPosition;
        }
    }

    // 检测玩家是否站在摆锤上
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // 获取玩家的 Transform 和 Rigidbody2D
            player = collision.transform;
            playerRigidbody = player.GetComponent<Rigidbody2D>();

        }
    }

    // 当玩家离开摆锤时，解除父子关系
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // 解锁玩家的旋转
            if (playerRigidbody != null)
            {
                playerRigidbody.constraints = RigidbodyConstraints2D.None;
            }
        }
    }
}

