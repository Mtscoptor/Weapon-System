using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FadeController : MonoBehaviour
{
    public Image fadeImage; // 负责黑屏的UI Image
    public float fadeDuration = 1f; // 渐变时间

    private void Awake()
    {
        if (fadeImage != null)
        {
            fadeImage.color = new Color(0, 0, 0, 0); // 初始透明
            fadeImage.gameObject.SetActive(true);
        }
    }

    public IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }

    public IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
}
