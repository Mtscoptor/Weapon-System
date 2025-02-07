using UnityEngine;
using UnityEngine.Tilemaps;


public class PlatformToggleAndMove2 : MonoBehaviour
{
    public bool initialState = true;       // 初始状态：是否显示
    public float toggleInterval = 2f;     // 切换状态的时间间隔（秒）
    public float moveDistance = 5f;       // 平台左右移动的距离
    public float moveSpeed = 2f;          // 平台移动速度

    private bool currentState;            // 当前状态
    private Vector3 initialPosition;      // 平台的初始位置
    private bool movingRight = false;      // 平台移动方向
    private Renderer platformRenderer;    // 平台的可视化组件
    private TilemapCollider2D platformCollider;    // 平台的碰撞体组件
    private Transform player; 

    void Start()
    {
        // 获取平台的组件
        platformRenderer = GetComponent<Renderer>();
        platformCollider = GetComponent<TilemapCollider2D>();

        // 设置初始状态和初始位置
        currentState = initialState;
        SetPlatformState(currentState);
        initialPosition = transform.position;

        // 启动状态切换的循环
        InvokeRepeating(nameof(TogglePlatformState), toggleInterval, toggleInterval);
    }

    void Update()
    {
        // 持续移动平台左右
        MovePlatform();
    }

    private void TogglePlatformState()
    {
        // 切换状态
        currentState = !currentState;
        SetPlatformState(currentState);

        // 输出状态信息（调试用）
        Debug.Log($"Platform is now {(currentState ? "Enabled" : "Disabled")}");
    }

    private void SetPlatformState(bool state)
    {
        // 设置 Renderer 和 Collider 的状态
        if (platformRenderer != null)
        {
            platformRenderer.enabled = state;
        }

        if (platformCollider != null)
        {
            platformCollider.enabled = state;
        }
        
        // 如果平台被禁用且玩家在上面，则取消父子关系
        if (!state && player != null)
        {
            player.SetParent(null);
            player = null;
        }
    }

    private void MovePlatform()
    {
        // 平台在初始位置两侧来回移动
        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            if (transform.position.x >= initialPosition.x)
            {
                movingRight = false; // 到达右边界，改变方向
            }
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            if (transform.position.x <= initialPosition.x - moveDistance)
            {
                movingRight = true; // 到达左边界，改变方向
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 如果玩家站在平台上，并且平台启用，则设置为子对象
        if (collision.collider.CompareTag("Player") && currentState)
        {
            player = collision.transform; // 获取玩家的 Transform
            player.SetParent(transform); // 设置玩家为平台的子对象
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 如果玩家离开平台，则取消父子关系
        if (collision.collider.CompareTag("Player") && player == collision.transform)
        {
            player.SetParent(null); // 取消父子关系
            player = null;          // 清空玩家引用
        }
    }
}
