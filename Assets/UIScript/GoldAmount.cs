using UnityEngine;
using TMPro;

public class GoldAmountDisplay : MonoBehaviour
{
    public TextMeshProUGUI goldText;  // 用于显示金币数量的TextMeshPro组件
    public PlayerStats playerStats;
    private int goldAmount;

    private void Start()
    {
        goldAmount = playerStats.coins;  // 初始金币数量
        UpdateGoldDisplay();
    }

    private void UpdateGoldDisplay()
    {
        goldText.text = "x" + goldAmount.ToString("000");
    }

    private void Update()
    {
        goldAmount = playerStats.coins;
        UpdateGoldDisplay();
    }
}