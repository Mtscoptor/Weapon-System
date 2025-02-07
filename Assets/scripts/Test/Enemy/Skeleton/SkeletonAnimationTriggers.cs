using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationTriggers : MonoBehaviour
{
    private Enemy_Skeleton enemy => GetComponentInParent<Enemy_Skeleton>();

    private void AnimationTrigger()
    {
        enemy.AnimationTrigger();
    }

    //private void AttackTrigger()
    //{
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheckPivot.position,
            //enemy.attackCheckRadius);
       //foreach (var hit in colliders)
        //{
            //if (hit.GetComponent<Player>() != null)
            //{
               // hit.GetComponent<Player>().Damaged(enemy.facingDir);
                //hit.GetComponent<Player>().stateMachine.ChangeState(hit.GetComponent<Player>().attackedState);
           // }
        //}
   // }

    private void OpenCounterAttackWindow() => enemy.OpenCounterAttackWindow();

    private void CloseCounterAttackWindow() => enemy.CloseCounterAttackWindow();
}
