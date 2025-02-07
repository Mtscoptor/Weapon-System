using UnityEngine;
using System.Collections;

public class PendulumSwing : MonoBehaviour
{
    public HingeJoint2D hingeJoint;  // 连接摆锤的HingeJoint2D组件
    public float baseMotorSpeed = 50f;   // 固定电机速度
    public float maxMotorForce = 500f;  // 最大电机扭矩
    public float lowerAngle = -45f;  // 最小角度
    public float upperAngle = 45f;   // 最大角度
    public float directionSwitchInterval = 0.5f;  // 最小时间间隔，单位：秒
    public float sleepTime = 0f;
    public float heightOffset = 1f;    // 角色的Y轴偏移量
    public PhysicsMaterial2D material1;  // 默认材质
    public PhysicsMaterial2D material2;  // 与摆锤接触时的材质
    public bool swingDirection = true;  // 控制摆锤的摆动方向
    
    private float lastSwitchTime = 0f;  // 上次切换方向的时间
    private float currentTime = 0f;  // 当前时间
    private Transform player;          // 角色的 Transform
    private Rigidbody2D playerRb;      // 角色的 Rigidbody2D

     void Start()
    {
        // 启动协程，等待指定时间
        StartCoroutine(WaitAndStart(sleepTime));  // 调用协程来休眠
    }

    // 定义一个协程来实现休眠
    IEnumerator WaitAndStart(float timeToWait)
    {
        // 输出休眠前的提示
        Debug.Log("Starting to sleep for " + timeToWait + " seconds...");

        // 休眠指定的时间
        yield return new WaitForSeconds(timeToWait);

        // 输出休眠结束的提示
        Debug.Log("Woke up after " + timeToWait + " seconds!");
        
        // 获取HingeJoint2D组件
        hingeJoint = GetComponent<HingeJoint2D>();

        // 启用电机
        JointMotor2D motor = hingeJoint.motor;
        motor.motorSpeed = baseMotorSpeed; // 设置初始电机速度
        motor.maxMotorTorque = maxMotorForce;
        hingeJoint.motor = motor;

        // 启用角度限制
        hingeJoint.useLimits = true;
        JointAngleLimits2D limits = hingeJoint.limits;
        limits.min = lowerAngle;
        limits.max = upperAngle;
        hingeJoint.limits = limits;
    }

    void Update()
    {
        // 获取当前时间
        currentTime = Time.time;

        // 控制电机的旋转方向，使摆锤来回摆动
        JointMotor2D motor = hingeJoint.motor;

        // 获取摆锤的当前角度
        float angle = hingeJoint.jointAngle;

        // 使用余弦函数平滑控制速度：角度越大，速度越小
        float speedFactor = Mathf.Cos((angle / upperAngle));  // 计算速度因子
        Debug.Log("Speed Factor: " + speedFactor);

        // 判断摆锤是否到达角度限制，且是否满足切换方向的时间间隔
        if ((angle <= lowerAngle || angle >= upperAngle) && (currentTime - lastSwitchTime >= directionSwitchInterval))
        {
            // 如果满足条件，反转摆锤的旋转方向
            swingDirection = !swingDirection;

            // 更新上次切换时间
            lastSwitchTime = currentTime;
        }

        // 计算最终的电机速度（使用固定基准速度，并调整因子）
        float adjustedMotorSpeed = baseMotorSpeed * speedFactor + 0.1f;

        // 根据摆动方向设置电机速度
        motor.motorSpeed = swingDirection ? adjustedMotorSpeed : -adjustedMotorSpeed;
        hingeJoint.motor = motor;
    }

    // 检测玩家是否站在摆锤上并切换玩家材质
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // 获取玩家的 Rigidbody2D 组件
            playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // 切换玩家的物理材质
                playerRb.sharedMaterial = material2;
            }

            // 保存玩家的 Transform
            player = collision.transform;
        }
    }

    // 当玩家离开摆锤时切换材质回默认材质
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // 检查玩家的 Rigidbody2D 是否存在
            if (playerRb != null)
            {
                // 恢复玩家的物理材质为默认材质
                playerRb.sharedMaterial = material1;
            }
        }
    }


}
