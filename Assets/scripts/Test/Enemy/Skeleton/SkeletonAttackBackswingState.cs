using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// skeleton的攻击后摇状态
public class SkeletonAttackBackswingState : EnemyState
{
    private Enemy_Skeleton enemy;

    public SkeletonAttackBackswingState(Enemy _enemyBase, EnemyStateMachine _stateMachine,
        string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(0, 0);

        if (CanAttack())
        {
            stateMachine.ChangeState(enemy.battleState);
            return;
        }

    }

    private bool CanAttack()
    {
        if (Time.time > enemy.lastAttackTime + enemy.attackCooldown)
            return true;
        else
            return false;
    }
}
