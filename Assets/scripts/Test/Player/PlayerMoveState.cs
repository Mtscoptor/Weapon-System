using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine,
        string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        rb.sharedMaterial = player.noFrictionMat;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {

        player.SetVelocity(player.slopeVec.x * xInput * player.moveSpeed
                , player.slopeVec.y * xInput * player.moveSpeed);

        base.Update();

        if (xInput == 0)
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }
    }
}