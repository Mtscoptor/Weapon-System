using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public PlayerStats playerStats;
    public Image fillImage;  // 血条图片
    public float maxHealth;
    public float currentHealth;


    private void Start()
    {
        currentHealth = playerStats.currentHealth;
        maxHealth = playerStats.maxHealth;
        UpdateHealthBar();
    }


    private void UpdateHealthBar()
    {
        fillImage.fillAmount = currentHealth / maxHealth;
    }

    
    private void Update()
    {
        currentHealth = playerStats.currentHealth;
        maxHealth = playerStats.maxHealth;
        UpdateHealthBar();
    }

}
