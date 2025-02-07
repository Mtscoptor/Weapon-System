using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    public LevelNameDisplay levelNameDisplay;
    public string levelName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 确保是玩家进入
        {
            levelNameDisplay.ShowLevelName(levelName);
            // 你可以在这里禁用触发器，以确保该显示效果只触发一次
            gameObject.SetActive(false);
        }
    }
}

