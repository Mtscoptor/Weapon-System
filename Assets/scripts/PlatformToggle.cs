using UnityEngine;

public class PlatformToggle : MonoBehaviour
{
    public bool initialState = true; // 初始状态：是否启用
    public float toggleInterval = 2f; // 切换状态的时间间隔（秒）

    private bool currentState;       // 当前状态

    void Start()
    {
        // 设置初始状态
        currentState = initialState;
        gameObject.SetActive(currentState);

        // 启动状态切换的循环
        InvokeRepeating(nameof(TogglePlatformState), toggleInterval, toggleInterval);
    }

    private void TogglePlatformState()
    {
        // 切换自身状态
        currentState = !currentState;
        gameObject.SetActive(currentState);

        // 输出状态信息（调试用）
        Debug.Log($"Platform is now {(currentState ? "Enabled" : "Disabled")}");
    }
}
