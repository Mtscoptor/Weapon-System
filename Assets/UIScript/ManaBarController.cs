using UnityEngine;
using UnityEngine.UI;

public class ManaBarController : MonoBehaviour
{
    public PlayerStats playerStats;
    public Image fillImage;  // 蓝条图片
    public float maxMana;
    public float currentMana = 50f;


    private void Start()
    {
        currentMana = PlayerStats.currentMana;
        UpdateManaBar();
    }


    private void UpdateManaBar()
    {
        fillImage.fillAmount = currentMana / maxMana;
    }

    
    private void Update()
    {
        currentMana = PlayerStats.currentMana;
        UpdateManaBar();
    }

}
