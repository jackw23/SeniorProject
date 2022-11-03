using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : State
{
    Transform enemyTransform, playerTransform, firePointTransform;
    float nextAttackTime = 0f;
    Vector3 attackDirection;
    float angle;

    public override void Enter(StateMachine stateMachine)
    {
        enemyTransform = stateMachine.transform;
        playerTransform = stateMachine.playerTransform;
        firePointTransform = stateMachine.enemy.firePoint;

        if (!stateMachine.enemy.constantAim) {
            attackDirection = playerTransform.position - firePointTransform.position;
            angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
            firePointTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
    }

    public override void Execute(StateMachine stateMachine)
    {
        if (Vector3.Distance(playerTransform.position, enemyTransform.position) < 5.0f && !stateMachine.enemy.constantAim) {
            if (Time.time > nextAttackTime) {
                if (enemyTransform.position.x - playerTransform.position.x > 0) {
                    enemyTransform.rotation = Quaternion.LookRotation(Vector3.back);
                } else {
                    enemyTransform.rotation = Quaternion.LookRotation(Vector3.forward);
                }
                
                attackDirection = playerTransform.position - firePointTransform.position;
                angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
                firePointTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                
                stateMachine.enemy.RangedAttack(); 
                nextAttackTime = Time.time + 1 / stateMachine.enemy.rangedAttackRate;
            }
        } else if (stateMachine.enemy.constantAim) {
            stateMachine.enemy.RangedAttack(); 
            nextAttackTime = Time.time + 1 / stateMachine.enemy.rangedAttackRate;
            stateMachine.nextState = stateMachine.idle;
            Exit(stateMachine);
        } else {
            stateMachine.nextState = stateMachine.idle;
            Exit(stateMachine);
        }
    }

    public override void Exit(StateMachine stateMachine)
    {
        stateMachine.enemy.firePoint.rotation = enemyTransform.rotation;
        stateMachine.TransitionState(stateMachine.nextState);
    }
}
