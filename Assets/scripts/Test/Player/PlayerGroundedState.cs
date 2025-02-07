using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mtscoptor.Weapons;

public class PlayerGroundedState : PlayerState
{

    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine,
    string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        player.isAirjump = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.m_CoyoteTime = player.tolerance;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.jumpState);
            return;
        }

        if (!player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.airState);
            player.isFall = true;
            return;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            player.isAttackInput = true;
            if (attackInputs[(int)CombatInputs.Whip])
                stateMachine.ChangeState(player.WhipAttackState);
            if (attackInputs[(int)CombatInputs.Knife])
                stateMachine.ChangeState(player.KnifeAttackState);
            if (attackInputs[(int)CombatInputs.Spear])
                stateMachine.ChangeState(player.SpearAttackState);
        }



        if (Input.GetKeyDown(KeyCode.E))
        {
            player.isAttackInput=true;
            if (attackInputs[(int)CombatInputs.Whip])
            {
                if (currentMana >= player.Whip_Heavy.weaponManaCost)
                {
                    stateMachine.ChangeState(player.WhipHeavyAttackState);
                    Debug.Log("WhipHeavyAttackState," + player.Whip_Heavy.weaponManaCost);
                }
                else Debug.Log("�������㣬�޷��ػ�");
            }

            if (attackInputs[(int)CombatInputs.Knife])
            {
                if (currentMana >= player.Knife_Heavy.weaponManaCost)
                {
                    stateMachine.ChangeState(player.KnifeHeavyAttackState);
                    Debug.Log("KnifeHeavyAttackState," + player.Knife_Heavy.weaponManaCost);
                }
                else Debug.Log("�������㣬�޷��ػ�");
            }

            if (attackInputs[(int)CombatInputs.Spear])
            {
                if (currentMana >= player.Spear_Heavy.weaponManaCost)
                {
                    stateMachine.ChangeState(player.SpearHeavyAttackState);
                    Debug.Log("SpearHeavyAttackState," + player.Spear_Heavy.weaponManaCost);
                }
                else Debug.Log("�������㣬�޷��ػ�");
            }
            Debug.Log("CurrentMana=" + currentMana);
        }

    }
}