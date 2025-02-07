using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_move : MonoBehaviour
{
    public float topPosition = 5f;        // 顶部位置的 y 值
    public float bottomPosition = -5f;    // 底部位置的 y 值
    public float downSpeed = 10f;         // 向下移动的速度（快速下落）
    public float upSpeed = 2f;            // 向上移动的速度（缓慢上升）
    public float stayTimeAtTop = 2f;      // 在顶部停留的时间
    public float stayTimeAtBottom = 2f;   // 在底部停留的时间

    private bool movingDown = true;       // 当前是否正在向下移动

    void Start()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (true)
        {
            // 快速向下移动
            if (movingDown)
            {
                while (transform.position.y > bottomPosition)
                {
                    transform.position = Vector3.MoveTowards(transform.position, 
                        new Vector3(transform.position.x, bottomPosition, transform.position.z), 
                        downSpeed * Time.deltaTime);
                    yield return null;
                }
                
                // 到达底部位置后停留
                yield return new WaitForSeconds(stayTimeAtBottom);
                movingDown = false;
            }
            // 缓慢向上移动
            else
            {
                while (transform.position.y < topPosition)
                {
                    transform.position = Vector3.MoveTowards(transform.position, 
                        new Vector3(transform.position.x, topPosition, transform.position.z), 
                        upSpeed * Time.deltaTime);
                    yield return null;
                }

                // 到达顶部位置后停留
                yield return new WaitForSeconds(stayTimeAtTop);
                movingDown = true;
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
