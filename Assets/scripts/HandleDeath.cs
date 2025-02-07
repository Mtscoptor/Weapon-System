using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RespawnSystem : MonoBehaviour
{
    public GameObject deathScreen; // 黑屏的 UI 面板
    public TextMeshProUGUI deathText; // 死亡提示文字
    public TextMeshProUGUI deathText2;
    public float fadeDuration = 1f; // 渐变的持续时间
    // public GameObject[] CheckPoints;
    public PlayerStats playerStats;
    public GameObject player;
    public GameObject statue;
    public GameObject initialRespawnPoint;
    public List<GameObject> respawnPoints = new List<GameObject>(); // 复活点列表

    void Start()
    {
        deathScreen.SetActive(false);
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerStats = player.GetComponent<PlayerStats>();
        }
    }
    

    // // 死亡动画协程
    // private IEnumerator HandleDeath()
    // {
    //     // 显示死亡画面并开始渐变黑
    //     statue = GetCurrentStatue();
    //     SavePoint statueScript = statue.GetComponent<SavePoint>();
    //     deathScreen.SetActive(true);
    //     if (statueScript!=null)
    //     {
    //         if (statueScript.remainingFlames>0)
    //         {
    //             deathText.text = "You died!\nFor Now..."; // 显示死亡提示
    //             deathText2.text ="The fire becomes weaker...";
    //         }
    //         else
    //         {
    //             deathText.text = "You died!\nFor Sure!"; // 显示死亡提示
    //             deathText2.text ="Better find another Statue...";
    //         }
    //     }
    //     else
    //     {
    //         deathText.text = "You died!\nFor Sure!"; // 显示死亡提示
    //         deathText2.text ="Better find another Statue...";
    //     }
        
    //     // 逐渐让黑屏的透明度增加
    //     CanvasGroup canvasGroup = deathScreen.GetComponent<CanvasGroup>();
    //     float timeElapsed = 0f;
    //     while (timeElapsed < fadeDuration)
    //     {
    //         canvasGroup.alpha = Mathf.Lerp(0, 1, timeElapsed / fadeDuration);
    //         timeElapsed += Time.deltaTime;
    //         yield return null;
    //     }
    //     canvasGroup.alpha = 1f;

    //     // 在黑屏期间复活并传送到存档点
    //     player.transform.position = statue.transform.position;
    //     Debug.Log("Player respawned at: " + statue.transform.position);
    //     playerStats.RestoreHealth(playerStats.maxHealth);
    //     playerStats.currentMana = 20f;
    //     if (statueScript!=null)
    //     {
    //         statueScript.remainingFlames--;
    //     }
        

    //     // 等待2秒后恢复正常画面
    //     yield return new WaitForSeconds(2f);

    //     // 逐渐将黑屏透明度减少，恢复画面
    //     timeElapsed = 0f;
    //     while (timeElapsed < fadeDuration)
    //     {
    //         canvasGroup.alpha = Mathf.Lerp(1, 0, timeElapsed / fadeDuration);
    //         timeElapsed += Time.deltaTime;
    //         yield return null;
    //     }
    //     canvasGroup.alpha = 0f;

    //     // 完成复活动画，隐藏死亡画面
    //     deathScreen.SetActive(false);
    //     playerStats.isDead = false;

    // }

    private GameObject GetCurrentStatue()
    {
        // 遍历列表，从最后一个有效雕像开始检查
        for (int i = respawnPoints.Count - 1; i >= 0; i--)
        {
            GameObject statue = respawnPoints[i];
            SavePoint statueScript = statue.GetComponent<SavePoint>();
            if (statueScript!=null)
            {
                if (statueScript.remainingFlames>0)
                {
                    return statue;
                }
            }
        }
        return initialRespawnPoint;
    }

    public void AddStatue(GameObject statue)
    {
        // 激活雕像时，将雕像加入复活点列表
        respawnPoints.Add(statue);
    }

    // 死亡动画协程
    private IEnumerator HandleDeath()
    {
    // 保存当前的时间比例
        float originalTimeScale = Time.timeScale;

    // 放慢游戏速度
        Time.timeScale = 0.2f; // 将游戏速度减慢至 50%

    // 显示死亡画面并开始渐变黑
        statue = GetCurrentStatue();
        SavePoint statueScript = statue.GetComponent<SavePoint>();
        deathScreen.SetActive(true);
        if (statueScript != null)
        {
            if (statueScript.remainingFlames > 0)
            {
                deathText.text = "You died!\nFor Now..."; // 显示死亡提示
                deathText2.text = "The fire becomes weaker...";
            }
            else
            {
                deathText.text = "You died!\nFor Sure!"; // 显示死亡提示
                deathText2.text = "Better find another Statue...";
            }
        }
        else
        {
            deathText.text = "You died!\nFor Sure!"; // 显示死亡提示
            deathText2.text = "Better find another Statue...";
        }

    // 逐渐让黑屏的透明度增加
        CanvasGroup canvasGroup = deathScreen.GetComponent<CanvasGroup>();
        float timeElapsed = 0f;
        while (timeElapsed < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, timeElapsed / fadeDuration);
            timeElapsed += Time.unscaledDeltaTime; // 使用 unscaledDeltaTime 以便不受 Time.timeScale 影响
            yield return null;
        }
        canvasGroup.alpha = 1f;

    // 在黑屏期间复活并传送到存档点
        player.transform.position = statue.transform.position;
        Debug.Log("Player respawned at: " + statue.transform.position);
        playerStats.RestoreHealth(playerStats.maxHealth);
        PlayerStats.currentMana = 20f;
        if (statueScript != null)
        {
            statueScript.remainingFlames--;
        }

    // 等待2秒后恢复正常画面
        yield return new WaitForSecondsRealtime(2f); // 使用 WaitForSecondsRealtime 以便不受 Time.timeScale 影响

    // 逐渐将黑屏透明度减少，恢复画面
        timeElapsed = 0f;
        while (timeElapsed < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, timeElapsed / fadeDuration);
            timeElapsed += Time.unscaledDeltaTime; // 使用 unscaledDeltaTime 以便不受 Time.timeScale 影响
            yield return null;
        }
        canvasGroup.alpha = 0f;

    // 完成复活动画，隐藏死亡画面
        deathScreen.SetActive(false);
        playerStats.isDead = false;

    // 恢复游戏速度
        Time.timeScale = originalTimeScale; // 恢复原来的游戏速度
    }

    public void Die()
    {

        StartCoroutine(HandleDeath()); // 开始死亡动画
    }

} 

