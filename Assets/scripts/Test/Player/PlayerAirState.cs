using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mtscoptor.Weapons;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine,
        string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {

        base.Enter();
        player.currentXvelocity = rb.velocity.x;
        player.fallSpeed = rb.velocity.x / 2;

        if (!player.isAttackInput)
        {
            player.SetVelocity(rb.velocity.x, rb.velocity.y); 
        }
        else
        {
            // ����й������룬ֱ�ӽ��빥��״̬
            //stateMachine.ChangeState(player.primaryAttackState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.m_CoyoteTime -= Time.deltaTime;
        if (player.m_CoyoteTime > 0 && Input.GetKeyDown(KeyCode.Space) && player.isFall)
        {
            player.isAirjump = true;
            stateMachine.ChangeState(player.jumpState);
            return;

        }

        if (player.isFall)
        {
            player.SetVelocity(player.fallSpeed, rb.velocity.y);
        }
        

        if (!player.isFall)
        {
            player.CheckEdge();
        }

        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
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
            player.isAttackInput = true;
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