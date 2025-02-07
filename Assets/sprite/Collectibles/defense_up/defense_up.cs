using UnityEngine;
using TMPro;

public class DefenseUpgrade : MonoBehaviour
{
    // public int defenseIcreaseAmount = 2;  // 提升的生命值量
    public GameObject dialogBox;  // 提示框
    public TextMeshProUGUI dialogText;  // 用于显示提示内容的TextMeshPro组件
    public string message = "Your defense has increased!";
    private bool isPlayerNearby = false;  // 判断玩家是否靠近
    public PlayerStats playerStats;

    private void Start()
    {
        dialogBox.SetActive(false);  // 初始化时隐藏对话框
    }

    private void Update()
    {
        // 玩家接触道具时按下W键进行拾取
        if (isPlayerNearby)
        {
            PickupItem();
        }
    }

    private void PickupItem()
    {
        playerStats.IncreaseDefensePower(1);

        // 显示对话框提示
        dialogBox.SetActive(true);
        dialogText.text = message;

        // 延迟2秒后隐藏对话框
        Invoke("HideDialog", 2f);
        
        // 销毁道具
        gameObject.SetActive(false);  // 销毁该物品

        Invoke("DestroyPickup",3f);
    }

    private void HideDialog()
    {
        Debug.Log("Hiding dialog box");
        dialogBox.SetActive(false);  // 隐藏对话框
        dialogText.text = "";  // 清空文本内容
    }

    private void DestroyPickup()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            // playerController = other.GetComponent<PlayerController>();  // 获取玩家控制器
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}

