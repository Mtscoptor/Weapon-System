using UnityEngine;

public class Lift : MonoBehaviour
{
    // 顶层到中间层的距离
    public float topToMiddleDistance = 3f;

    // 中间层到底层的距离
    public float middleToBottomDistance = 4f;

    // 移动速度
    public float moveSpeed = 2f;

    // 当前电梯的层级（0 = 最低层, 1 = 中间层, 2 = 最高层）
    private int currentLevel = 2;

    // 是否正在移动
    private bool isMoving = false;

    // 角色检测
    private bool isCharacterOnElevator = false;
    private Transform character; // 当前附着的角色

    // 电梯的墙（子对象的 Edge Collider 2D）
    public GameObject elevatorWalls;

    public Camera camera1; // 初始启用的镜头1
    public Camera camera2; // 移动时切换到的镜头2

    void Start()
    {
        elevatorWalls.SetActive(false);
    }

    void Update()
    {
        if (isMoving || !isCharacterOnElevator) return; // 电梯移动中或角色不在电梯上时，不响应按键

        // 上箭头：向上移动
        if (Input.GetKeyDown(KeyCode.UpArrow) && currentLevel < 2)
        {
            MoveToNextLevel(1); // 向上移动一层
        }

        // 下箭头：向下移动
        if (Input.GetKeyDown(KeyCode.DownArrow) && currentLevel > 0)
        {
            MoveToNextLevel(-1); // 向下移动一层
        }
    }

    private void MoveToNextLevel(int direction)
    {
        // 根据当前层级和移动方向计算目标位置
        float targetY = transform.position.y;

        if (direction == 1) // 向上
        {
            targetY += currentLevel == 1 ? topToMiddleDistance : middleToBottomDistance;
        }
        else if (direction == -1) // 向下
        {
            targetY -= currentLevel == 2 ? topToMiddleDistance : middleToBottomDistance;
        }

        // 更新当前层级
        currentLevel += direction;

        // 开始移动
        isMoving = true;

        SwitchToCamera2();

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

        SwitchToCamera1();

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

    private void SwitchToCamera1()
    {
        if (camera1 != null) camera1.enabled = true;
        if (camera2 != null) camera2.enabled = false;
    }

    private void SwitchToCamera2()
    {
        if (camera1 != null) camera1.enabled = false;
        if (camera2 != null) camera2.enabled = true;
    }
}
