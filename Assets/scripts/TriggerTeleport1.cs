using UnityEngine;

public class TriggerTeleport1 : MonoBehaviour
{
    public Transform player; // 角色对象
    public Transform targetPosition; // 传送目的地
    public Camera mainCamera;
    public Camera secondaryCamera;
    public Vector3 offsetFromTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.position = targetPosition.position + offsetFromTrigger; // 传送角色

            mainCamera.enabled = false;
            secondaryCamera.enabled = true;
        }
    }
}
