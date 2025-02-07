using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetInitialRespawnPoint : MonoBehaviour
{
    private bool isActive = false;
    public RespawnSystem respawnSystem;
    public CameraFollow cameraFollow; // 引用 CameraFollow 脚本
    public bool isZoomedIn = false; // 记录当前镜头是否放大
    public float fixedY = -4;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "Level1")
            {
                cameraFollow.SetZoom(isZoomedIn);
                cameraFollow.SetFollowY(isZoomedIn);
                cameraFollow.SetFixedY(fixedY);
            }
            
            // 设置初始复活点
            if (respawnSystem != null && !isActive)
            {
                respawnSystem.AddStatue(gameObject);
                isActive = true;
            }
        }
    }
}