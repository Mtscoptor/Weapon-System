using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class player_control2 : MonoBehaviour
{
    public float moveSpeed = 5f; // 控制角色移动速度
    public LayerMask groundLayer; // 定义地面层，用于射线检测
    private Vector2 colliderOffset; // 碰撞体的偏移量
    private Rigidbody2D rb2D; // 角色的刚体组件

// Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); // 获取刚体组件
        colliderOffset = GetComponent<BoxCollider2D>().offset; // 获取碰撞体偏移量

    }

    private void Move(float velocity)
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // 获取水平输入
        bool isFacingRight = horizontalInput >= 0; // 判断角色是否面向右侧

        // 计算目标位置（水平移动）
        Vector2 targetPosition = (Vector2)transform.position + new Vector2(horizontalInput, 0f) * moveSpeed * Time.deltaTime;

        // 射线检测起点，目标位置上方
        Vector2 rayOrigin = targetPosition + new Vector2(0f, 0.5f); 

        // 发射向下的射线，检测地形高度
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, 1f, groundLayer);

        // 碰撞体实际位置，包含偏移量
        Vector2 currentPosition = (Vector2)transform.position + colliderOffset;

        // 确定目标位置：若射线检测命中，取命中点；否则取目标位置
        Vector2 actualTargetPosition = hit.collider != null ? hit.point : targetPosition;

        // 计算移动方向（归一化）
        Vector2 moveDirection = (actualTargetPosition - currentPosition).normalized;

        // 如果面向左侧，取反 x 坐标（疑似多余）
        if (!isFacingRight) moveDirection.x *= -1;

        // 更新刚体速度
        rb2D.velocity = moveDirection * moveSpeed;
    }

    private void FixedUpdate()
    {
        Move(moveSpeed); // 调用 Move 方法
    }


}
