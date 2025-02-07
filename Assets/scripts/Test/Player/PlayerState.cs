using Mtscoptor.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    private Core core { get; set; }
    protected Player player;
    protected PlayerStats playerStats { get; private set; }
    protected PlayerStateMachine stateMachine;
    static public bool[] attackInputs { get; private set; } = new bool[] { false, false, false, false, false, false };
    private string animBoolName;
    protected float currentMana;
    private Movement movement;
    protected bool triggerCalled;
    protected Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }

    protected float xInput;
    protected float yInput;

    protected Rigidbody2D rb;
    protected float stateTimer;
    public enum CombatInputs
    {
        Whip,
        Whip_Heavy,
        Knife,
        Knife_Heavy,
        Spear,
        Spear_Heavy
    }

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        playerStats = new PlayerStats();
        currentMana = PlayerStats.currentMana;
        movement = new Movement();
        // ��֤״̬��Enter()��Ҫ�õ�������Ϣʱ���������µ�
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
        triggerCalled = false;
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        player.anim.SetFloat("yVelocity", rb.velocity.y);

        player.SlopeCheck();
        movement.CheckIfShouldFlip((int)xInput);
        currentMana = PlayerStats.currentMana;
        OnKnifeAttackInput();
        OnSpearAttackInput();
        OnWhipAttackInput();
        //if (Input.GetKeyDown(KeyCode.F)) { playerStats.GetWeapon("Sword"); Debug.Log("isSwordCollected = " + playerStats.GetIsWeaponCollected(1)); }

    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
    public void OnWhipAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && playerStats.SwitchWeapon(0))
        {
            attackInputs[(int)CombatInputs.Whip] = true;
            Debug.Log("1");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            attackInputs[(int)CombatInputs.Whip] = false;
        }
    }
    public void OnKnifeAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && playerStats.SwitchWeapon(1))
        {
            attackInputs[(int)CombatInputs.Knife] = true;
            Debug.Log("2");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            attackInputs[(int)CombatInputs.Knife] = false;
        }
    }
    public void OnSpearAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3) && playerStats.SwitchWeapon(2))
        {
            attackInputs[(int)CombatInputs.Spear] = true;
            Debug.Log("3");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            attackInputs[(int)CombatInputs.Spear] = false;
        }
    }
}