using UnityEngine;

public class super_camera : MonoBehaviour
{
    public Transform target; // 跟随目标
    public Vector3 offset = new Vector3(0, 0, -10); // 偏移
    public float smoothSpeed = 0.125f; // 平滑速度
    public PolygonCollider2D mapBounds; // 地图边界（PolygonCollider2D）

    private Camera cam;

    void Start()
    {
        cam = Camera.main; // 获取主相机
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            // 计算目标位置
            Vector3 desiredPosition = target.position + offset;

            // 平滑移动到目标位置
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // 限制相机位置
            Vector3 clampedPosition = ClampCameraPosition(smoothedPosition);

            transform.position = clampedPosition;
        }
        else
        {
            Debug.LogWarning("CameraFollow: Target not assigned.");
        }
    }

    Vector3 ClampCameraPosition(Vector3 position)
    {
        // 获取相机的可视范围
        float cameraHeight = 2f * cam.orthographicSize;
        float cameraWidth = cameraHeight * cam.aspect;

        // 计算相机视口四个角的位置
        Vector3 topLeft = position + new Vector3(-cameraWidth / 2f, cameraHeight / 2f, 0);
        Vector3 topRight = position + new Vector3(cameraWidth / 2f, cameraHeight / 2f, 0);
        Vector3 bottomLeft = position + new Vector3(-cameraWidth / 2f, -cameraHeight / 2f, 0);
        Vector3 bottomRight = position + new Vector3(cameraWidth / 2f, -cameraHeight / 2f, 0);

        // 限制四个角的坐标，使其都在地图边界内
        Vector3 clampedPosition = position;

        // 获取四个角的最小/最大X和Y值
        float minX = mapBounds.bounds.min.x + cameraWidth / 2f;
        float maxX = mapBounds.bounds.max.x - cameraWidth / 2f;
        float minY = mapBounds.bounds.min.y + cameraHeight / 2f;
        float maxY = mapBounds.bounds.max.y - cameraHeight / 2f;

        // 限制相机的x和y坐标
        clampedPosition.x = Mathf.Clamp(position.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(position.y, minY, maxY);

        return clampedPosition;
    }
}
