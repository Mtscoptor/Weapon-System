using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Enemy_Skeleton enemy;

    private Transform playerTransform;
    private int moveDir;

    public SkeletonBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine,
        string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.battleExitTime;

        // playerTransform = GameObject.Find("Player").transform;
        playerTransform = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleExitTime;
            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                stateMachine.ChangeState(enemy.attackState);
                return;
            }
        }
        else
        {
            // enemy丢失player视野后，若二者相距太远或enemy丢失视野时间过长，则enemy脱离battleState状态
            if (stateTimer < 0 || 
                Vector2.Distance(playerTransform.position, enemy.transform.position) > enemy.battleExitDistance)
            {
                stateMachine.ChangeState(enemy.moveState);
                return;
            }
        }

        if (playerTransform.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if (playerTransform.position.x < enemy.transform.position.x)
            moveDir = -1;

        enemy.SetVelocity(moveDir * enemy.moveSpeed, rb.velocity.y);
    }
}
