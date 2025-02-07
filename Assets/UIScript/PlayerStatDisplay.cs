using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatsDisplay : MonoBehaviour
{
    public GameObject statsPanel; // 属性显示面板
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI defenseText;
    public TextMeshProUGUI coinsText;

    public PlayerStats playerStats;
    private bool isPaused = false;

    private void Start()
    {

        // 确保面板一开始是隐藏的
        if (statsPanel != null)
        {
            statsPanel.SetActive(false);
        }
    }

    private void Update()
    {
        // 按下Tab键时显示或隐藏面板
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleStatsPanel();
        }
    }

    private void ToggleStatsPanel()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // 显示面板并更新数据
            UpdateStats();
            statsPanel.SetActive(true);
            Time.timeScale = 0; // 暂停游戏
        }
        else
        {
            // 隐藏面板并恢复游戏
            statsPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    private void UpdateStats()
    {
        if (playerStats == null) return;

        healthText.text = $"HP: {playerStats.currentHealth}/{playerStats.maxHealth}";
        manaText.text = $"MP: {PlayerStats.currentMana}/{playerStats.maxMana}";
        attackText.text = $"ATK: +{playerStats.attackPower}";
        defenseText.text = $"DEF: {playerStats.defensePower}";
        coinsText.text = $"GOLD: {playerStats.coins}";
    }

}
