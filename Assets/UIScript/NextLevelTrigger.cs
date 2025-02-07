using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    private bool isPlayerNearby = false; 
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    public PlayerStats playerStats;
    public string nextSceneName; // 下一个关卡的名称
    // public FadeController fadeController; // 渐变效果的控制器

    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        dialogText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.W))
        {
            if (playerStats.haveGoldenKey)
            {
                playerStats.haveGoldenKey = false;
                // PlayerController playerController = FindObjectOfType<PlayerController>();
                // if (playerController != null)
                // {
                //     playerController.enabled = false;
                // }
                SceneManager.LoadScene(nextSceneName);
                // // 开始渐暗并加载下一关
                // StartCoroutine(TransitionToNextLevel());
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = "You need the golden key";
                Invoke("HideDialog", 2f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
                            // 检查玩家是否已经被标记为DontDestroyOnLoad
            if (!IsDontDestroyOnLoad(playerStats.gameObject))
            {
                DontDestroyOnLoad(playerStats.gameObject);
                Debug.Log("玩家已被标记为Don't Destroy On Load");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    private void HideDialog()
    {
        dialogBox.SetActive(false);
        dialogText.text = "";
    }

    private bool IsDontDestroyOnLoad(GameObject obj)
    {
        return obj.transform.root.gameObject == SceneManager.GetActiveScene().GetRootGameObjects()[0];
    }


    // private IEnumerator TransitionToNextLevel()
    // {
    //     // 禁用玩家操作
    //     PlayerController playerController = FindObjectOfType<PlayerController>();
    //     if (playerController != null)
    //     {
    //         playerController.enabled = false;
    //     }

    //     // 渐暗效果
    //     if (fadeController != null)
    //     {
    //         yield return fadeController.FadeIn();
    //     }

    //     // 加载下一关
    //     SceneManager.LoadScene(nextSceneName);

    //     // 渐亮效果
    //     if (fadeController != null)
    //     {
    //         yield return fadeController.FadeOut();
    //     }

    //     // 启用玩家操作
    //     if (playerController != null)
    //     {
    //         playerController.enabled = true;
    //     }
    // }
}
