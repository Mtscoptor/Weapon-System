using UnityEngine;
using TMPro;  // 引入TextMeshPro命名空间

public class SignPost : MonoBehaviour
{
    public TMP_Text messageText;  // 显示讯息的TextMeshPro组件
    public GameObject messageBox;  // 讯息显示框（UI面板）
    public string message = "";  // 需要显示的讯息
    private bool isPlayerInRange = false;
    private bool isMessageBoxActive = false;
    // public PlayerController playerController;

    private void Start()
    {
        // 确保游戏开始时，显示框是隐藏的
        messageBox.SetActive(false);
        messageText.text="";
    }

    private void Update()
    {
        if (isPlayerInRange)
        {
            // 如果玩家处于告示牌触发范围并且消息框尚未显示
// 玩家按下W键显示框
            if(!isMessageBoxActive)
            {
                ShowMessageBox();
            }
            if(isMessageBoxActive && Input.GetKeyDown(KeyCode.W))
            {
                HideMessageBox();
                Destroy(gameObject);
            }
            
        }
        if (isMessageBoxActive && !isPlayerInRange)
        {
            // 如果消息框处于活动状态，监听关闭操作// 玩家按下Space键关闭显示框
            
            HideMessageBox();
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // 确保是玩家触发
        {
            isPlayerInRange = true;
            // playerController=other.GetComponent<PlayerController>();
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void ShowMessageBox()
    {
        // 显示消息框并显示讯息
        if (messageBox != null && messageText != null)
        {
            messageText.text = message;  // 设置讯息内容
            messageBox.SetActive(true);  // 显示讯息框
            isMessageBoxActive = true;   // 标记为已显示
            // DisablePlayerController();
        }
    }
    private void HideMessageBox()
    {
        // 隐藏消息框
        if (messageBox != null)
        {
            messageBox.SetActive(false);  // 隐藏显示框
            messageText.text="";
            isMessageBoxActive = false;   // 标记为已关闭    
        }
        
    }
    // private void DisablePlayerController()
    // {
    //     if (playerController!=null)
    //     {
    //         playerController.inDialog=true;
    //     }
    // }
    // private void EnablePlayerController()
    // {
    //     if (playerController!=null)
    //     {
    //         playerController.inDialog=false;
    //     }
    // }
}

