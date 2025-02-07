using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LevelNameDisplay : MonoBehaviour
{
    public TextMeshProUGUI levelNameText; // 拖入你的TextMeshProUGUI对象
    public float fadeDuration = 1.5f; // 淡入淡出持续时间

    private void Start()
    {
        levelNameText.alpha = 0; // 初始文字不可见
    }

    public void ShowLevelName(string levelName)
    {
        StartCoroutine(FadeInAndOut(levelName));
    }

    private IEnumerator FadeInAndOut(string levelName)
    {
        levelNameText.text = levelName; // 设置关卡名称
        yield return StartCoroutine(FadeTextToFullAlpha(fadeDuration)); // 淡入
        yield return new WaitForSeconds(1f); // 保持文字显示1秒
        yield return StartCoroutine(FadeTextToZeroAlpha(fadeDuration)); // 淡出
    }

    private IEnumerator FadeTextToFullAlpha(float duration)
    {
        float elapsedTime = 0;
        Color targetColor = new Color(levelNameText.color.r, levelNameText.color.g, levelNameText.color.b, 1);
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            levelNameText.color = Color.Lerp(levelNameText.color, targetColor, elapsedTime / duration);
            yield return null;
        }
        levelNameText.color = targetColor;
    }

    private IEnumerator FadeTextToZeroAlpha(float duration)
    {
        float elapsedTime = 0;
        Color targetColor = new Color(levelNameText.color.r, levelNameText.color.g, levelNameText.color.b, 0);
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            levelNameText.color = Color.Lerp(levelNameText.color, targetColor, elapsedTime / duration);
            yield return null;
        }
        levelNameText.color = targetColor;
    }
}

