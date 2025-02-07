using UnityEngine;

public class CameraSwitchTrigger1 : MonoBehaviour
{
    public Camera camera1; // 前一个镜头
    public Camera camera2; // 初始禁用的镜头2
    public GameObject trigger5; // 需要禁用的对象（Trigger 5）


    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查进入触发器的对象是否为玩家
        if (other.CompareTag("Player"))
        {
            SwitchToCamera2(); // 切换到镜头2

            // 禁用Trigger 5对象
            if (trigger5 != null)
                trigger5.SetActive(false); // 禁用整个Trigger 5对象

            Debug.Log("Trigger 5 object disabled.");
        }
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
