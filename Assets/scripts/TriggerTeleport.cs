using UnityEngine;

public class TriggerTeleport : MonoBehaviour
{
    public Transform player; // 角色对象
    public Transform targetPosition; // 传送目的地
    public Camera mainCamera;
    public Camera secondaryCamera;
    public Vector3 offsetFromTrigger;

    private void Start()
    {
        // 初始化摄像机状态，假设 mainCamera 为默认启用，secondaryCamera 为禁用
        mainCamera.enabled = true;
        secondaryCamera.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.position = targetPosition.position + offsetFromTrigger; // 传送角色
            if (gameObject.name == "Trigger1")
            {
                mainCamera.enabled = false;
                secondaryCamera.enabled = true;
            }
            else if (gameObject.name == "Trigger2")
            {
                mainCamera.enabled = true;
                secondaryCamera.enabled = false;
            }
        }
    }
}
