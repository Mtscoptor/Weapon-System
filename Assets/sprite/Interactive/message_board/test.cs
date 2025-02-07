using UnityEngine;
using TMPro;  // 引入TextMeshPro命名空间

public class SignPostTest : MonoBehaviour
{
    public TMP_Text messageText;  // 显示讯息的TextMeshPro组件
    public GameObject messageBox;  // 讯息显示框（UI面板）
    public string message = "这里是告示牌提示！";  // 需要显示的讯息
    private bool isMessageBoxActive = false;  // 判断显示框是否打开

    void Update()
    {
        // 玩家按下M键时，显示讯息框并销毁告示牌
        if (Input.GetKeyDown(KeyCode.M))
        {
            ShowMessageBox();
        }

        // 如果消息框处于活动状态，监听关闭操作
        if (isMessageBoxActive && Input.GetKeyDown(KeyCode.Space))  // 玩家按下Space键关闭显示框
        {
            HideMessageBox();
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
            gameObject.SetActive(false);  // 销毁告示牌对象
        }
    }

    private void HideMessageBox()
    {
        // 隐藏消息框
        if (messageBox != null)
        {
            messageBox.SetActive(false);  // 隐藏显示框
            isMessageBoxActive = false;   // 标记为已关闭
        }
    }
}
