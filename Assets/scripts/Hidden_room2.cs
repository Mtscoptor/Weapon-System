using UnityEngine;

public class Hidden_room_2 : MonoBehaviour
{
    public Transform player; // 角色对象
    public GameObject level1Player;
    public Transform targetPosition; // 传送目的地
    public Camera mainCamera;
    public Camera secondaryCamera;
    public Vector3 offsetFromTrigger;

    private bool playerInTrigger = false; // 标记角色是否在触发区域内

    private void Start()
    {
        // 初始化摄像机状态，假设 mainCamera 为默认启用，secondaryCamera 为禁用
        secondaryCamera.enabled = false;
        if (level1Player==null)
        {
            level1Player = GameObject.FindGameObjectWithTag("Player");
            if (level1Player!=null)
            {
                player=level1Player.transform;
            }  
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true; // 标记角色进入触发区域
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false; // 标记角色离开触发区域
        }
    }

    private void Update()
    {
        // 检查是否按下上箭头键，并且角色在触发区域内
        if (playerInTrigger && Input.GetKeyDown(KeyCode.UpArrow))
        {
            player.position = targetPosition.position + offsetFromTrigger; // 传送角色

            mainCamera.enabled = false;
            secondaryCamera.enabled = true;
            
        }
    }
}
