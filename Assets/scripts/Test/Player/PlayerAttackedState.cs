using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackedState : PlayerState
{
    public PlayerAttackedState(Player _player, PlayerStateMachine _stateMachine,
        string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.fx.InvokeRepeating("RedBlink", 0, .2f);

        stateTimer = player.knockbackDuration;
    }

    public override void Exit()
    {
        base.Exit();

        player.fx.Invoke("CancelRedBlink", 0);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0 && player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }
    }
}
