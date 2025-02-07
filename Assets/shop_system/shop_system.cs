using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public int itemPrice; // 道具价格
    public string itemName; // 道具名称
    public GameObject dialogBox; // 对话框对象
    public TextMeshProUGUI dialogText; // 用于显示提示内容的TextMeshPro组件
    // public TextMeshProUGUI priceText; // 用于显示价格的TextMeshPro组件
    private bool isPlayerNearby; // 检测玩家是否在范围内
    private bool isDialogActive; // 检测对话框是否打开
    private bool isPurchased;
    public string feedbackMessage="You Have unlocked the Spear";
    public PlayerStats playerStats;
    // private PlayerController playerController;

    private void Start()
    {
        dialogBox.SetActive(false); // 初始隐藏对话框
        dialogText.text="";
        // priceText.text=$"{itemPrice}g";
    }

    private void Update()
    {
        // priceText.transform.position = transform.position + new Vector3(0, 1.5f, 0);
        // 玩家在范围内并按下 W 键
        if (isPlayerNearby && !isDialogActive)
        {
            OpenDialog();
        }
        if (!isPlayerNearby && isDialogActive)
        {
            CloseDialog();
        }
        // 玩家确认购买
        if (isDialogActive && Input.GetKeyDown(KeyCode.W))
        {
            AttemptPurchase();

        }
    }

    private void OpenDialog()
    {
        isDialogActive = true;
        dialogBox.SetActive(true);
        if (itemPrice > playerStats.coins)
        {
            dialogText.text = $"Will you buy a {itemName} \nfor <color=red>{itemPrice}</color> gold?\n<color=red>W=yes</color>";
        }
        else
        {
            dialogText.text = $"Will you buy a {itemName} \nfor {itemPrice} gold?\nW=yes";
        }
    }

    private void CloseDialog()
    {
      
        
        
        if (isPurchased)
        {
            isPurchased=false;
            // Destroy(priceText);
            dialogText.text=feedbackMessage;
            Invoke("CloseDialog",2f);
            gameObject.SetActive(false);
            Invoke("DestroyPickup",3f);
        }
        else
        {
            isDialogActive = false;
            dialogBox.SetActive(false);
            dialogText.text="";    
        }
   
    }

    private void DestroyPickup()
    {
        Destroy(gameObject);
    }

    private void AttemptPurchase()
    {
        if (playerStats.coins != null && itemPrice <= playerStats.coins)
        {
            playerStats.AddGold(-itemPrice);
            isPurchased=true;
            if (itemName == "Spear") FindObjectOfType<PlayerStats>().GetWeapon("Spear"); 
            CloseDialog();
        }
        else
        {
            dialogText.text="Not enough cash!";
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

}
