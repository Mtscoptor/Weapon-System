using UnityEngine;

public class CameraFollow3 : MonoBehaviour
{
    public GameObject player;
    public Transform target;  // 跟随目标
    public Vector3 offset = new Vector3(0, 0, -10);  // 偏移
    public float smoothSpeed = 0.125f;  // 平滑速度

    void Start()
    {
        if (player == null)
        {
            player= GameObject.FindGameObjectWithTag("Player"); 
            target = player.transform;
        }
        
        
        
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            // 计算目标位置
            Vector3 desiredPosition = target.position + offset;

            // 平滑移动到目标位置
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // 根据y坐标值设置x坐标的最大值、最小值和y的最大值
            float maxX = GetMaxXBasedOnY(smoothedPosition.y);
            float minX = GetMinXBasedOnY(smoothedPosition.y);
            float maxY = GetMaxYBasedOnY(smoothedPosition.y);

            // 限制相机的x坐标最大值和最小值，以及y坐标的最大值
            smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minX, maxX);
            smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, float.NegativeInfinity, maxY);

            // 更新相机位置
            transform.position = smoothedPosition;
        }
        else
        {
            Debug.LogWarning("CameraFollow: Target not assigned.");
        }
    }

    // 根据y坐标返回对应的最大x值
    float GetMaxXBasedOnY(float y)
    {
        float maxX = 10.7f;
        return maxX;
    }

    // 根据y坐标返回对应的最小x值
    float GetMinXBasedOnY(float y)
    {
        float minX = -44f;
        return minX;
    }

    // 根据y坐标返回对应的最大y值
    float GetMaxYBasedOnY(float y)
    {
        float maxY = 0f;
        maxY = Mathf.Infinity;  // 默认情况下没有限制
        return maxY;
    }
}
