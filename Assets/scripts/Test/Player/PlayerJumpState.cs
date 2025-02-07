using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerJumpState : PlayerState
{


    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine,
        string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.isFall = false;


        player.isJumping = true;
        player.jumpTimeCounter = 0f;


        if (player.isAirjump)
        {
            player.SetVelocity(player.currentXvelocity, player.jumpForce);
        }
        else
        {
            player.SetVelocity(rb.velocity.x, player.jumpForce);
        }

    }

    public override void Exit()
    {

        base.Exit();
        player.isJumping = false;
    }

    public override void Update()
    {
        base.Update();

        player.jumpTimeCounter += Time.deltaTime;


        if (Input.GetButton("Jump") && player.jumpTimeCounter < player.maxJumpTime)
        {

            rb.velocity = new Vector2(rb.velocity.x, player.jumpHoldForce);
        }

        if (!Input.GetButton("Jump") || player.jumpTimeCounter >= player.maxJumpTime)
        {

            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * player.jumpReleaseMultiplier);
            stateMachine.ChangeState(player.airState);
        }

        if (rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
        }


        if (Input.GetKeyDown(KeyCode.J))
        {
            player.isAttackInput = true;
        }
    }

    private float Min(float num1, float num2)
    {
        if (num1 < num2)
            return num1;

        return num2;
    }


}
