using UnityEngine;

public class LadderControl : MonoBehaviour
{
    public GameObject ladder2;       // 初始隐藏的梯子
    public GameObject ladder3;       // 初始显示的梯子
    public GameObject ladder4;       // 初始显示的梯子
    public GameObject trigger1;      // 触发器
    private Rigidbody2D ladder3Rb;   // ladder3 的 Rigidbody2D 组件
    private Rigidbody2D ladder4Rb;   // ladder3 的 Rigidbody2D 组件
    private bool isTriggered = false; // 是否触发了 trigger1
    private float dropDistance = 5f;  // ladder3 下落的距离
    private float dropSpeed = 5f;     // ladder3 下落的速度（控制物理引擎响应）

    void Start()
    {
        ladder2.SetActive(false);  // 确保 ladder2 初始隐藏
        ladder3.SetActive(true);   // 确保 ladder3 初始显示
        ladder4.SetActive(true);   // 确保 ladder3 初始显示
        ladder3Rb = ladder3.GetComponent<Rigidbody2D>();  // 获取 ladder3 的 Rigidbody2D 组件
        ladder3Rb.bodyType = RigidbodyType2D.Static; // 初始时设置为静态
        ladder4Rb = ladder4.GetComponent<Rigidbody2D>();  // 获取 ladder3 的 Rigidbody2D 组件
        ladder4Rb.bodyType = RigidbodyType2D.Static; // 初始时设置为静态
    }

    void Update()
    {
        // 如果触发器被触发，并且按下了 D 键
        if (isTriggered && Input.GetKeyDown(KeyCode.F))
        {
            // 启动下落操作
            StartCoroutine(DropLadderAndSwitch());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 如果玩家触碰到 trigger1
        if (other.CompareTag("Player"))
        {
            isTriggered = true;  // 设置为已触发
        }
    }

    private System.Collections.IEnumerator DropLadderAndSwitch()
    {
        // 将 ladder3 的 Rigidbody2D 设置为动态，使其响应物理引擎
        ladder3Rb.bodyType = RigidbodyType2D.Dynamic;
        ladder4Rb.bodyType = RigidbodyType2D.Dynamic;
        ladder2.SetActive(true);
        // 让 ladder3 开始下落，使用物理引擎的重力作用
        yield return new WaitForSeconds(1f); // 等待1秒让 ladder3 下落

        // 隐藏 ladder3，显示 ladder2
        ladder3.SetActive(false);
        ladder4.SetActive(false);

        // 将 ladder3 的 Rigidbody2D 设置回静态，使其停止运动
        ladder3Rb.bodyType = RigidbodyType2D.Static;
        ladder4Rb.bodyType = RigidbodyType2D.Static;
    }
}
