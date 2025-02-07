using UnityEngine;

public class TriggerTeleport2 : MonoBehaviour
{
    public CameraFollow cameraFollow;
    public Transform player; // 角色对象
    public GameObject level1Player;
    public Transform targetPosition; // 传送目的地
    public Camera mainCamera;
    public Camera secondaryCamera;
    public Vector3 offsetFromTrigger;

    void Start()
    {
        if (level1Player==null)
        {
            level1Player = GameObject.FindGameObjectWithTag("Player");
            player = level1Player.transform;
        }
    } 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.position = targetPosition.position + offsetFromTrigger; // 传送角色

            mainCamera.enabled = false;
            cameraFollow.SetFixedY(25);
            secondaryCamera.enabled = true;
        }
    }
}
