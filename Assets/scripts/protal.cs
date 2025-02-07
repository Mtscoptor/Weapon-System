using UnityEngine;

public class protal : MonoBehaviour
{
    public GameObject player; // 角色对象
    public Transform targetPosition; // 传送目的地
    public Camera mainCamera;
    public Camera secondaryCamera;
    public Vector3 offsetFromTrigger;
    
    private void Start()
    {
        if (player== null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.transform.position = targetPosition.position + offsetFromTrigger; // 传送角色
            mainCamera.enabled = false;
            secondaryCamera.enabled = true;
        }
    }
}
