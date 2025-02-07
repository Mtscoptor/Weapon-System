using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Move info")]
    public float moveSpeed;
    public float idleTime;

    [Header("Battle info")]
    [SerializeField] protected float playerCheckDistance; // enemy的视野索player的距离
    [SerializeField] protected LayerMask playerMask;
    public float battleCheckDistance; // enemy丢失player视野时(如player在其后背)的索player的距离
    public float battleExitTime; // enemy丢失player视野时的最小脱battleState时间
    public float battleExitDistance; // enemy丢失player视野时的最小脱battleState距离

    [Header("Attack info")]
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector] public float lastAttackTime;

    [Header("Injured info")]
    public float stunnedDuration;
    public Vector2 stunnedPow;
    protected bool canBeStunned;
    [SerializeField] protected GameObject counterImage;

    #region States
    public EnemyStateMachine stateMachine { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();
    }

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheckPivot.position,
        Vector2.right * facingDir, playerCheckDistance, playerMask);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,
            new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    }

    public virtual void OpenCounterAttackWindow()
    {
        canBeStunned = true;
        counterImage.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;
        counterImage.SetActive(false);
    }

    public virtual void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

}
