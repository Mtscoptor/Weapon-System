using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 角色的 Transform
    public Vector3 offset = new Vector3(0, 0, -10); // 镜头初始偏移量
    public float zoomedOutSize = 5f; // 正常状态下的镜头大小
    public float zoomedInSize = 10f; // 放大状态下的镜头大小
    public bool isZoomedIn = false; // 判断当前是否放大
    public bool followY = false; // 控制Y轴是否跟随角色
    public float minY = 0f;

    private Camera cam;
    public float fixedY; // 固定的 Y 值

    void Start()
    {
        cam = GetComponent<Camera>();
        fixedY = -4; // 初始时 Y 值
        cam.orthographicSize = zoomedOutSize; // 设置初始的镜头缩放大小
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // 根据 followY 是否跟随角色的 Y 轴
            bool tmpfollowY = followY || (target.position.y >4 && target.position.y <18);
            if(target.position.y<4 && !followY)fixedY = -4;
            float changedY = minY > target.position.y ? minY : target.position.y;
            float yPosition = tmpfollowY ? changedY : fixedY;
            float xPosition = 176 < target.position.x ? 176 : target.position.x;
            xPosition = xPosition > -60 ? xPosition : -60;
            if(fixedY==25)xPosition = xPosition > -33 ? xPosition : -33;
            yPosition = 17 < yPosition && tmpfollowY ? 17 : yPosition;
            // 设置镜头位置
            transform.position = new Vector3(xPosition, yPosition, target.position.z + offset.z);

            // 检查是否触发放大状态
            cam.orthographicSize = isZoomedIn ? zoomedInSize : zoomedOutSize;
        }
    }

    // 外部方法来控制放大或缩小
    public void SetZoom(bool zoomIn)
    {
        isZoomedIn = zoomIn;
    }

    // 外部方法来控制是否跟随Y轴
    public void SetFollowY(bool shouldFollowY)
    {
        followY = shouldFollowY;
    }
    public void SetFixedY(float outFixedY)
    {
        fixedY = outFixedY;
    }
}
