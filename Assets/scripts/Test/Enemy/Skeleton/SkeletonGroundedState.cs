using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundedState : EnemyState
{
    protected Enemy_Skeleton enemy;

    protected Transform playerTransform;

    public SkeletonGroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine,
        string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

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

        if (enemy.IsPlayerDetected() || 
            Vector2.Distance(playerTransform.position, enemy.transform.position) < enemy.battleCheckDistance)
        {
            stateMachine.ChangeState(enemy.battleState);
            return;
        }
    }
}
