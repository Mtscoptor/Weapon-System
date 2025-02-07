using UnityEngine;
using TMPro;

public class SavePoint : MonoBehaviour
{
    public GameObject messageBox; // 提示玩家保存的UI
    public TextMeshProUGUI message;
    private bool isPlayerInRange = false; // 玩家是否在存档点范围内
    private bool isActive = false;
    public GameObject[] fireEffects;
    public int remainingFlames;
    
    public RespawnSystem respawnSystem;

    // public PlayerController playerController;  // 引用玩家脚本


    private void Start()
    {
        isActive = false;
        if (messageBox != null)
        {
            messageBox.SetActive(false); // 确保提示框初始为隐藏状态
            message.text = "";
        }
        if (fireEffects != null)
        {
            foreach (var fireEffect in fireEffects)
            {
                fireEffect.SetActive(false);
            }
        }
    }

    private void Update()
    {
        // if (playerController != null && isActive)
        // {
        //     // 根据火焰数量来显示/隐藏火焰特效
        //     int remainingFlames = playerController.remainingFlames;
        if (isActive)
        {
            // 确保火焰特效的数量与剩余的火焰数量一致
            for (int i = 0; i < fireEffects.Length; i++)
            {
                if (i < remainingFlames)
                {
                    fireEffects[i].SetActive(true); // 激活火焰特效
                }
                else
                {
                    fireEffects[i].SetActive(false); // 关闭火焰特效
                }
            }

            // if (isActive && remainingFlames <0)
            // {
            //     Destroy(gameObject);
            // }
        }

        // 如果玩家在范围内并按下W键，激活存档点
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.W))
        {
            if (!isActive)
            {
                ActivateSavePoint();
            }
            else
            {
                messageBox.SetActive(true);
                if (remainingFlames>0)
                {
                    message.text = "the fire still burns...";
                }
                else
                {
                    message.text = "Nothing happens";
                }
                Invoke("HideMessageBox",2f);
            }
           
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (messageBox != null && !isActive)
            {
                messageBox.SetActive(true); // 显示提示框
                message.text = "Will you pray at the statue?\nW=yes";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (messageBox != null)
            {
                messageBox.SetActive(false); // 隐藏提示框
                message.text = "";
            }
        }
    }

    private void ActivateSavePoint()
    {
        Debug.Log("Save point activated!");
        isActive = true;
        remainingFlames = 2;
        respawnSystem.AddStatue(gameObject);

        if (message != null)
        {
            message.text = "Your soul will finally return...";
        }

        // 隐藏提示框
        if (messageBox != null)
        {
            Invoke("HideMessageBox", 2f);
        }
    }

    private void HideMessageBox()
    {
        messageBox.SetActive(false);
        message.text = "";
    }
}
