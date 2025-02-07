using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mtscoptor.CoreSystem;
using Mtscoptor.Weapons;

public class Player : Entity
{
    [Header("Attack details")]
    public Vector2[] attackMovement;
    public float attackBackswingDur;
    public PlayerState playerState { get; private set; }
    public float counterAttackDuration;
    public bool isBusy { get; private set; }
    [HideInInspector] public bool isAttackInput = false; 

    [Header("Move info")]
    public float moveSpeed;
    public float jumpForce;
    public float fallSpeed;
    public float tolerance = 0.05f;
    public float m_CoyoteTime;
    public bool isFall;
    public float edgeDistance = 0.5f;  
    public float edgeSnapDistance = 1f;  
    private bool isNearEdge;
    public float currentXvelocity;
    public bool isAirjump;


    [Header("Jump Control")]
    public float maxJumpTime = 0.5f;   
    public float jumpHoldForce = 5f; 
    public float jumpReleaseMultiplier = 0.5f; 
    [HideInInspector]
    public bool isJumping = false;   
    [HideInInspector]
    public float jumpTimeCounter = 0; 


    [Header("Dash info")]
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }


    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerAttackState WhipAttackState { get; private set; }
    public PlayerAttackState KnifeAttackState { get; private set; }
    public PlayerAttackState SpearAttackState { get; private set; }
    public PlayerAttackState WhipHeavyAttackState { get; private set; }
    public PlayerAttackState KnifeHeavyAttackState { get; private set; }
    public PlayerAttackState SpearHeavyAttackState { get; private set; }
    public PlayerAttackedState attackedState { get; private set; }
    // public PlayerPreJumpState preJumpState { get; private set; }
    #endregion

    #region Components
    #endregion

    protected override void Awake()
    {
        base.Awake();

        Core = GetComponent<Core>();

        stateMachine = new PlayerStateMachine();

        Whip = transform.Find("Whip").GetComponent<Weapon>();
        Knife = transform.Find("Knife").GetComponent<Weapon>();
        Spear = transform.Find("Spear").GetComponent<Weapon>();
        Whip_Heavy = transform.Find("Whip_Heavy").GetComponent<Weapon>();
        Knife_Heavy = transform.Find("Knife_Heavy").GetComponent<Weapon>();
        Spear_Heavy = transform.Find("Spear_Heavy").GetComponent<Weapon>();

        Whip.SetCore(Core);
        Knife.SetCore(Core);
        Spear.SetCore(Core);
        Whip_Heavy.SetCore(Core);
        Knife_Heavy.SetCore(Core);
        Spear_Heavy.SetCore(Core);


        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        WhipAttackState = new PlayerAttackState(this, stateMachine, "Attack", Whip, PlayerState.CombatInputs.Whip, false);
        KnifeAttackState = new PlayerAttackState(this, stateMachine, "Attack", Knife, PlayerState.CombatInputs.Knife, false);
        SpearAttackState = new PlayerAttackState(this, stateMachine, "Attack", Spear, PlayerState.CombatInputs.Spear, false);
        WhipHeavyAttackState = new PlayerAttackState(this, stateMachine, "Attack", Whip_Heavy, PlayerState.CombatInputs.Whip_Heavy, true);
        KnifeHeavyAttackState = new PlayerAttackState(this, stateMachine, "Attack", Knife_Heavy, PlayerState.CombatInputs.Knife_Heavy, true);
        SpearHeavyAttackState = new PlayerAttackState(this, stateMachine, "Attack", Spear_Heavy, PlayerState.CombatInputs.Spear_Heavy, true);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();
    }

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public void CheckEdge()
    {

        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, edgeSnapDistance, groundLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, edgeSnapDistance, groundLayer);

        if ((hitLeft.collider != null && hitLeft.distance <= edgeSnapDistance) ||
            (hitRight.collider != null && hitRight.distance <= edgeSnapDistance))
        {
            isNearEdge = true;
        }
        else
        {
            isNearEdge = false;
        }

        if (isNearEdge && IsGroundDetected())
        {
            SnapToEdge();
        }
    }

    public void SnapToEdge()
    {
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, edgeSnapDistance, groundLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, edgeSnapDistance, groundLayer);

        if (hitLeft.collider != null && hitLeft.distance <= edgeSnapDistance)
        {
            transform.position = new Vector3(hitLeft.point.x + edgeDistance, transform.position.y, transform.position.z);
        }
        else if (hitRight.collider != null && hitRight.distance <= edgeSnapDistance)
        {
            transform.position = new Vector3(hitRight.point.x - edgeDistance, transform.position.y, transform.position.z);
        }
    }

}