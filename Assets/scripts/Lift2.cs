using UnityEngine;

public class Lift2 : MonoBehaviour
{
    // 顶层和底层的距离
    public float topToBottomDistance = 7f;

    // 移动速度
    public float moveSpeed = 2f;

    // 当前电梯的层级（0 = 底层, 1 = 顶层）
    private int currentLevel = 1;

    // 是否正在移动
    private bool isMoving = false;

    // 角色检测
    private bool isCharacterOnElevator = false;
    private Transform character; // 当前附着的角色

    // 电梯的墙（子对象的 Edge Collider 2D）
    public GameObject elevatorWalls;

    // 电梯的触发器区域（用于解除角色与电梯的父子关系）
    public Collider2D liftTriggerArea;

    void Start()
    {
        elevatorWalls.SetActive(false);
    }

    void Update()
    {
        if (isMoving || !isCharacterOnElevator) return; // 电梯移动中或角色不在电梯上时，不响应按键

        // 上箭头：向上移动
        if (Input.GetKeyDown(KeyCode.UpArrow) && currentLevel == 0)
        {
            MoveToNextLevel(1); // 向上移动到顶层
        }

        // 下箭头：向下移动
        if (Input.GetKeyDown(KeyCode.DownArrow) && currentLevel == 1)
        {
            MoveToNextLevel(0); // 向下移动到底层
        }
    }

    private void MoveToNextLevel(int targetLevel)
    {
        // 根据目标层级计算目标位置
        float targetY = targetLevel == 1
            ? transform.position.y + topToBottomDistance // 移动到顶层
            : transform.position.y - topToBottomDistance; // 移动到底层

        // 更新当前层级
        currentLevel = targetLevel;

        // 开始移动
        isMoving = true;

        // 启用电梯的墙
        if (elevatorWalls != null)
        {
            elevatorWalls.SetActive(true);
        }

        // 将角色附着到电梯
        if (character != null)
        {
            character.SetParent(transform);
        }

        StartCoroutine(MoveElevator(targetY));
    }

    private System.Collections.IEnumerator MoveElevator(float targetY)
    {
        // 平滑移动到目标位置
        while (Mathf.Abs(transform.position.y - targetY) > 0.01f)
        {
            transform.position = new Vector2(transform.position.x, Mathf.MoveTowards(transform.position.y, targetY, moveSpeed * Time.deltaTime));
            yield return null;
        }

        // 确保位置完全对齐
        transform.position = new Vector2(transform.position.x, targetY);
        isMoving = false;

        // 禁用电梯的墙
        if (elevatorWalls != null)
        {
            elevatorWalls.SetActive(false);
        }

        // 解除角色与电梯的父子关系
        if (character != null)
        {
            character.SetParent(null);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) // 假设角色的标签是 "Player"
        {
            isCharacterOnElevator = true;
            character = collision.transform; // 记录角色的 Transform
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isCharacterOnElevator = false;
            character = null; // 清空角色引用
        }
    }

    // 当角色进入电梯触发器时，解除父子关系
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player")) // 假设角色的标签是 "Player"
        {
            other.transform.SetParent(null);
            Debug.Log("Player jumped out!");
        }
    }
}
