using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class StartMenuController : MonoBehaviour
{
    public TextMeshProUGUI startText; // “按下任意键开始”的Text
    public Image fadeImage; // 渐暗的Image
    public string nextSceneName; // 要加载的场景名称
    public float fadeDuration = 1f; // 渐暗持续时间

    private bool isStarting = false; // 防止多次触发

    void Start()
    {
        // 开始时启动闪烁效果
        StartCoroutine(TextBlink());
    }

    void Update()
    {
        if (!isStarting && Input.anyKeyDown)
        {
            StartCoroutine(StartGame());
        }
    }

    IEnumerator TextBlink()
    {
        while (!isStarting)
        {
            // 切换Text透明度实现闪烁
            startText.color = new Color(startText.color.r, startText.color.g, startText.color.b, 0f);
            yield return new WaitForSeconds(0.2f);
            startText.color = new Color(startText.color.r, startText.color.g, startText.color.b, 1f);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator StartGame()
    {
        isStarting = true;
        // 渐暗效果
        float timer = 0f;
        Color fadeColor = fadeImage.color;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeColor.a = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            fadeImage.color = fadeColor;
            yield return null;
        }

        // 加载下一场景
        SceneManager.LoadScene(nextSceneName);
    }
}
