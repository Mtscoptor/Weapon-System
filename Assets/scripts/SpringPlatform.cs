using UnityEngine;

public class SpringPlatform : MonoBehaviour
{
    public float moveDistance = 5f; // 平台移动的距离
    public float moveSpeed = 2f;    // 平台移动的速度

    private Vector3 initialPosition;       // 平台的初始位置
    private Vector3 targetPosition;        // 平台的目标位置
    private Transform player;              // 玩家对象
    private bool isMovingUp = false;       // 平台是否正在向上移动
    private bool isMovingDown = false;     // 平台是否正在向下移动

    void Start()
    {
        // 记录平台的初始位置和目标位置
        initialPosition = transform.position;
        targetPosition = initialPosition + Vector3.up * moveDistance;
    }

    void Update()
    {
        // 平台向上移动
        if (isMovingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // 当平台到达目标位置时，停止向上移动
            if (transform.position == targetPosition)
            {
                isMovingUp = false;
            }
        }

        // 平台向下移动
        if (isMovingDown)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, moveSpeed * Time.deltaTime);

            // 当平台回到初始位置时，停止向下移动
            if (transform.position == initialPosition)
            {
                isMovingDown = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            player = collision.transform; // 获取玩家的 Transform

            // 将玩家设为平台的子对象
            player.SetParent(transform);

            // 延迟0.25秒后开始向上移动
            Invoke(nameof(StartMovingUp), 0.25f);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && player == collision.transform)
        {
            // 玩家离开平台时，解除父子关系
            player.SetParent(null);
            player = null;

            // 平台开始向下移动
            isMovingDown = true;
            isMovingUp = false;
        }
    }

    private void StartMovingUp()
    {
        if (player != null)
        {
            isMovingUp = true;   // 平台开始向上移动
            isMovingDown = false; // 停止向下移动
        }
    }
}
