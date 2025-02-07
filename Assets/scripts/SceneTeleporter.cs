using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleporter : MonoBehaviour
{
    public string targetSceneName; // 目标场景的名称

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 检查是否为角色碰撞
        {
            SceneManager.LoadScene(targetSceneName); // 加载指定场景
        }
    }
}
