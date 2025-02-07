using UnityEngine;

public class CameraZoomTrigger : MonoBehaviour
{
    public CameraFollow cameraFollow; // 引用 CameraFollow 脚本
    public bool isZoomedIn = false; // 记录当前镜头是否放大
    public float fixedY = -4;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 每次进入触发区域时切换镜头状态
            isZoomedIn = cameraFollow.isZoomedIn;
            isZoomedIn = !isZoomedIn;
            cameraFollow.SetZoom(isZoomedIn); // 根据状态放大或缩小镜头
            cameraFollow.SetFollowY(isZoomedIn);
            cameraFollow.SetFixedY(fixedY);
        }
    }
}
