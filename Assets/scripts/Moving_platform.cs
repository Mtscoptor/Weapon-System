using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Moving_platform : MonoBehaviour
{
    public float speed = 2f; // 平台移动速度
    public float moveDistance = 5f; // 平台的移动距离

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingToTarget = true;

    void Start()
    {
        // 初始化起始位置和目标位置
        startPosition = transform.position;
        targetPosition = startPosition + Vector3.right * moveDistance;
    }

    void Update()
    {
        // 平台在两个位置之间来回移动
        if (movingToTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                movingToTarget = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, startPosition) < 0.1f)
            {
                movingToTarget = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 将角色设为平台的子对象
            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 解除子对象关系
            collision.transform.SetParent(null);
        }
    }
}
