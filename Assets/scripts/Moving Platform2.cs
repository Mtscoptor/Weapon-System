using UnityEngine;
using System.Collections;

public class MovingPlatform2 : MonoBehaviour
{
    public float moveUpDistance = 3f;      // 向上移动的距离
    public float moveLeftDistance = 5f;    // 向左移动的距离
    public float moveSpeed = 2f;           // 平台的移动速度
    public float waitTime = 0.5f;          // 平台向左移动后保持静止的时间

    private bool isPlayerOnPlatform = false;  // 检测角色是否站在平台上
    private bool isMoving = false;            // 平台是否正在移动
    private Transform player;                 // 角色的 Transform

    void Update()
    {
        // 检测角色是否按下“上箭头”，并且角色站在平台上且平台未在移动
        if (isPlayerOnPlatform && !isMoving && Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(MovePlatform());
        }
    }

    // 检测角色是否站在平台上
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            player = collision.transform;  // 获取角色的 Transform
            isPlayerOnPlatform = true;      // 设置角色站在平台上
            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isPlayerOnPlatform = false;     // 设置角色离开平台
            collision.transform.SetParent(null);
        }
    }

    // 协程：实现平台的顺序移动
    IEnumerator MovePlatform()
    {
        isMoving = true;  // 标记平台正在移动

        // 1. 向上移动
        Vector3 startPosition = transform.position;
        Vector3 targetPositionUp = startPosition + Vector3.up * moveUpDistance;
        float journeyLengthUp = Vector3.Distance(startPosition, targetPositionUp);
        float startTimeUp = Time.time;

        while (Vector3.Distance(transform.position, targetPositionUp) > 0.1f)
        {
            float distanceCoveredUp = (Time.time - startTimeUp) * moveSpeed;
            float fractionOfJourneyUp = distanceCoveredUp / journeyLengthUp;
            transform.position = Vector3.Lerp(startPosition, targetPositionUp, fractionOfJourneyUp);
            yield return null;
        }

        transform.position = targetPositionUp;  // 确保平台准确到达目标位置

        // 2. 向左移动
        Vector3 startPositionLeft = transform.position;
        Vector3 targetPositionLeft = startPositionLeft + Vector3.left * moveLeftDistance;
        float journeyLengthLeft = Vector3.Distance(startPositionLeft, targetPositionLeft);
        float startTimeLeft = Time.time;

        while (Vector3.Distance(transform.position, targetPositionLeft) > 0.1f)
        {
            float distanceCoveredLeft = (Time.time - startTimeLeft) * moveSpeed;
            float fractionOfJourneyLeft = distanceCoveredLeft / journeyLengthLeft;
            transform.position = Vector3.Lerp(startPositionLeft, targetPositionLeft, fractionOfJourneyLeft);
            yield return null;
        }

        transform.position = targetPositionLeft;  // 确保平台准确到达目标位置

        // 3. 等待一段时间后，平台停止移动
        yield return new WaitForSeconds(waitTime);
        //isMoving = false;  // 标记平台停止移动
    }
}
