using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Mtscoptor.Weapons;

public class PlayerAttackState : PlayerState
{
    private int inputIndex;
    private int comboCounter;
    private Weapon weapon;
    private float lastAttackTime;
    private float comboWindow = 2;

    private float lastHeavyAttackTime;
    public bool isHeavyAttack;

    public PlayerAttackState(Player _player, PlayerStateMachine _stateMachine,
        string _animBoolName, Weapon weapon,
        CombatInputs input,
        bool isHeavyAttack) : base(_player, _stateMachine, _animBoolName)
    {
        this.weapon = weapon;
        inputIndex = (int)input;
        weapon.OnExit += ExitHandler;
        this.isHeavyAttack = isHeavyAttack;
    }

    public override void Enter()
    {
        base.Enter();

        weapon.Enter();
        player.isAttackInput = false;

        if (Time.time > lastAttackTime + comboWindow)
            comboCounter = 0;
        player.anim.SetInteger("ComboCounter", comboCounter);


        float attackDir = player.facingDir;
        if (xInput != 0)
            attackDir = xInput;


        if (this.isHeavyAttack)
        {

            HeavyAttack();
        }


        if (player.IsGroundDetected())
        {
            stateTimer = .1f;

            if (attackInputs[(int)CombatInputs.Spear] && this.isHeavyAttack)
            {
                player.SetVelocity(player.attackMovement[comboCounter].x * attackDir * 5,
                                   player.attackMovement[comboCounter].y);
            }
            else
            {
                player.SetVelocity(player.attackMovement[comboCounter].x * attackDir,
                                   player.attackMovement[comboCounter].y);
            }
        }



    }

    public override void Exit()
    {
        base.Exit();

        comboCounter = (comboCounter + 1) % 1;
        lastAttackTime = Time.time;

        // 设置攻击后摇
        player.StartCoroutine("BusyFor", player.attackBackswingDur);
    }

    public override void Update()
    {
        base.Update();

        if (player.IsGroundDetected() && stateTimer < 0)
            player.SetVelocity(0, 0);

        if (triggerCalled)
        {
            if (player.IsGroundDetected())
                stateMachine.ChangeState(player.idleState);
            else
                stateMachine.ChangeState(player.airState);
            return;
        }

    }
    private void ExitHandler()
    {
        AnimationFinishTrigger();
    }
    void HeavyAttack()
    {
        Debug.Log("重击！");
        lastHeavyAttackTime = Time.time;
        
        playerStats.UseMana(this.weapon.weaponManaCost);

    }
}
