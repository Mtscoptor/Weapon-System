using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour
{
    public float xMoveSpeed = 3f; // 固定的左右移动速度
    public float yMoveSpeed = 20f; // 固定的上下移动速度
    public float playerGravityScale = 5f; // 玩家平时的重力比例
    public Player player;

    public GameObject playerGameObject;

    private bool isOnLadder = false; // 玩家是否在梯子上
    private Rigidbody2D playerRb; // 引用玩家的 Rigidbody2D


    void Start()
    {
        if (playerGameObject == null)
        {
            playerGameObject = GameObject.FindGameObjectWithTag("Player");
            player = playerGameObject.GetComponent<Player>();
            if (player == null)
            {
                Debug.LogError("未找到 Player 组件！");
            }
        }
        else
        {
            Debug.LogError("未找到 Player 游戏对象！");
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        float vertical = Input.GetAxisRaw("Vertical");
        if (other.CompareTag("Player") && !isOnLadder && vertical!=0)
        {
            isOnLadder = true;
            playerRb = other.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Debug.Log("玩家进入梯子触发器。");
                // 禁用玩家的重力，以便控制上下移动
                playerRb.gravityScale = 0;
            }
            else
            {
                Debug.LogError("未找到玩家的 Rigidbody2D 组件！");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isOnLadder = false;
            if (playerRb != null)
            {
                // 恢复玩家的重力比例
                playerRb.gravityScale = playerGravityScale;
                playerRb = null;
            }
            Debug.Log("玩家离开梯子触发器。");
        }
    }

    void Update()
    {
        if (isOnLadder && playerRb != null)
        {
            player.stateMachine.ChangeState(player.idleState);
            // 检测玩家是否按下空格键（跳跃）
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // 玩家开始跳跃，禁用梯子控制
                isOnLadder = false;
                playerRb.gravityScale = playerGravityScale;
                Debug.Log("玩家在梯子上跳跃。");

                // 启动协程以在跳跃后重新检测是否仍在梯子上
                StartCoroutine(RecheckLadderAfterJump());
            }
        }

        if (isOnLadder && playerRb != null)
        {
            HandleLadderMovement();
        }
    }

    /// <summary>
    /// 处理玩家在梯子上的移动
    /// </summary>
    private void HandleLadderMovement()
    {
        // 获取输入轴
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // 更新玩家的速度
        Vector2 newVelocity = playerRb.velocity;
        newVelocity.x = horizontal * xMoveSpeed;
        newVelocity.y = vertical * yMoveSpeed;
        playerRb.velocity = newVelocity;

        // 玩家没有按键时，角色坐标不变
        if (horizontal == 0 && vertical == 0)
        {
            playerRb.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// 在玩家跳跃后重新检测是否仍在梯子上
    /// </summary>
    private IEnumerator RecheckLadderAfterJump()
    {
        // 等待一小段时间以允许跳跃动作开始
        yield return new WaitForSeconds(0.5f);

        if (playerRb != null)
        {
            // 获取玩家的 Collider2D
            Collider2D playerCollider = playerRb.GetComponent<Collider2D>();
            if (playerCollider != null)
            {
                // 检查玩家是否仍然与梯子的触发器重叠
                bool stillOnLadder = false;

                // 使用 OverlapBox 检测玩家是否仍在梯子触发器内
                BoxCollider2D ladderTrigger = GetComponent<BoxCollider2D>();
                if (ladderTrigger != null)
                {
                    Vector2 position = ladderTrigger.bounds.center;
                    Vector2 size = ladderTrigger.bounds.size;

                    // 获取 "Player" 层的掩码
                    LayerMask playerLayer = LayerMask.GetMask("Player");

                    // 检测是否有玩家的 Collider2D 重叠在梯子的触发器内
                    Collider2D[] overlappingColliders = Physics2D.OverlapBoxAll(position, size, 0, playerLayer);

                    foreach (Collider2D collider in overlappingColliders)
                    {
                        if (collider.CompareTag("Player"))
                        {
                            stillOnLadder = true;
                            break;
                        }
                    }

                    if (stillOnLadder)
                    {
                        isOnLadder = true;
                        playerRb.gravityScale = 0;
                        Debug.Log("玩家在跳跃后仍在梯子上。");
                    }
                    else
                    {
                        isOnLadder = false;
                        playerRb.gravityScale = playerGravityScale;
                        Debug.Log("玩家在跳跃后离开了梯子。");
                    }
                }
                else
                {
                    Debug.LogError("梯子的 BoxCollider2D 未找到！");
                }
            }
            else
            {
                Debug.LogError("玩家的 Collider2D 未找到！");
            }
        }
    }
}
