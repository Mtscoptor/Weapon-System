using UnityEngine;

public class move_up1 : MonoBehaviour
{
    public float moveDistance = 5f; // 平台移动的距离
    public float moveSpeed = 2f;    // 平台移动的速度

    public Vector3 initialPosition;       // 平台的初始位置
    public Vector3 targetPosition;        // 平台的目标位置
    private Transform player;              // 玩家对象
    private bool isMoving = false;         // 平台是否正在移动
    public PlayerStats playerStats;
    public bool isMoved = false;

    void Start()
    {
        // 记录平台的初始位置和目标位置
        initialPosition = transform.position;
        targetPosition = initialPosition + Vector3.up * moveDistance;
    }

    void Update()
    {
        // 如果平台正在移动
        if (isMoving)
        {
            // 平台向上移动
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // 当平台到达目标位置时，停止移动
            if (transform.position == targetPosition)
            {
                isMoving = false;
                isMoved = true;

                // 解除玩家与平台的父子关系
                if (player != null)
                {
                    player.SetParent(null);
                    player = null;
                }
            }
        }
        if (isMoved && playerStats.isDead)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, moveSpeed * Time.deltaTime);
        }
        if (isMoved && playerStats.isStuck)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, moveSpeed * Time.deltaTime);
        }
        
        
    }

    // 检测玩家是否站在平台上
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isMoving)
        {
            isMoving = true; // 开始移动平台
            player = other.transform; // 获取玩家的 Transform

            // 将玩家设为平台的子对象
            player.SetParent(transform);
        }
    }

    // 当玩家离开平台时，仍保持为子对象直到平台移动结束
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && player == other.transform && isMoving)
        {
            // 玩家离开平台时仍然保持为子对象，直到平台移动结束
            player.SetParent(transform);
        }
    }
}
