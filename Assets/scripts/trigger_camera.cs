using UnityEngine;

public class CameraSwitchTrigger : MonoBehaviour
{
    public Camera camera1; // 初始启用的镜头1
    public Camera camera2; // 初始禁用的镜头2


    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查进入触发器的对象是否为玩家
        if (other.CompareTag("Player"))
        {
            SwitchToCamera2(); // 切换到镜头2
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 检查离开触发器的对象是否为玩家
        if (other.CompareTag("Player"))
        {
            SwitchToCamera1(); // 切换回镜头1
        }
    }

    private void SwitchToCamera1()
    {
        if (camera1 != null)
            camera1.enabled = true;
        if (camera2 != null)
            camera2.enabled = false;

        Debug.Log("Switched to Camera 1");
    }

    private void SwitchToCamera2()
    {
        if (camera1 != null)
            camera1.enabled = false;
        if (camera2 != null)
            camera2.enabled = true;

        Debug.Log("Switched to Camera 2");
    }
}
