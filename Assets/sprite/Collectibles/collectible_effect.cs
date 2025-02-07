using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    public float floatAmplitude = 5f; // 浮动的幅度
    public float floatSpeed = 2.5f;       // 浮动的速度

    private Vector3 initialPosition;    // 记录初始位置

    void Start()
    {
        // 记录物体的初始位置
        initialPosition = transform.position;
    }

    void Update()
    {
        // 计算浮动的偏移量
        float offsetY = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;

        // 更新物体的位置（只改变 Y 轴位置）
        transform.position = new Vector3(initialPosition.x, initialPosition.y + offsetY, initialPosition.z);
    }
}
