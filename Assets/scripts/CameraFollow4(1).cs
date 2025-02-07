using UnityEngine;

public class CameraFollow4 : MonoBehaviour
{
    public Transform target;  // 跟随目标
    public GameObject player;
    public Vector3 offset = new Vector3(0, 5, -10);  // 偏移
    public float smoothSpeed = 0.125f;  // 平滑速度

    void Start()
    {
        if (player ==null)
        
        {
            player= GameObject.FindGameObjectWithTag("Player");
            target=player.transform;
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

            if(target.position.x > 450)
            {
                smoothedPosition.x = 127.0434f;
                smoothedPosition.y = 14.04417f;
            }
            
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
        float maxX = 0;

        // 使用 switch-case 来根据y值的不同范围设置最大x值
        switch (y)
        {
            case float n when (n > -40f && n <= 40f):
                maxX = 301.1f;  // y在-40到40之间时，最大x值为331
                break;
            case float n when (n > 40f):
                maxX = 209f;  // y大于40时，最大x值为201
                break;
            case float n when (n >= -60f && n <= -40f):
                maxX = 331f;  // y在-60到-40之间时，最大x值为301.1
                break;
            case float n when (n >= -130f && n <= -100f):
                maxX = 349.3f;  // y在-130到-100之间时，最大x值为349.3
                break;
            default:
                maxX = Mathf.Infinity;  // 默认情况下没有限制
                break;
        }

        return maxX;
    }

    // 根据y坐标返回对应的最小x值
    float GetMinXBasedOnY(float y)
    {
        float minX = 0;

        // 使用 switch-case 来根据y值的不同范围设置最小x值
        switch (y)
        {
            case float n when (n < 35f):
                minX = 0f;  
                break;
            case float n when (n >= 35f):
                minX = 31.7f; 
                break;
            default:
                minX = Mathf.NegativeInfinity;  // 默认情况下没有限制
                break;
        }

        return minX;
    }

    // 根据y坐标返回对应的最大y值
    float GetMaxYBasedOnY(float y)
    {
        float maxY = 0;

        // 使用 switch-case 来根据y值的不同范围设置最大y值
        switch (y)
        {
            case float n when (n > 256.8f):
                maxY = 16.5f;  // y在-40到40之间时，最大y值为100
                break;
            case float n when (n <= 256.8f):
                maxY = 74f;  // y大于40时，最大y值为150
                break;
            default:
                maxY = Mathf.Infinity;  // 默认情况下没有限制
                break;
        }

        return maxY;
    }
}
