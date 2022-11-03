using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/State/Chase")]
public class ChaseState : State
{

    Transform enemyTransform;
    Transform playerTransform;
    public float movementSpeed = 1.0f;
    
    public override void Enter(StateMachine stateMachine) 
    {
        enemyTransform = stateMachine.transform;
        playerTransform = stateMachine.playerTransform;

        if (enemyTransform.position.x - playerTransform.position.x > 0) {
            enemyTransform.rotation = Quaternion.LookRotation(Vector3.back);
        } else {
            enemyTransform.rotation = Quaternion.LookRotation(Vector3.forward);
        }
    }

    public override void Execute(StateMachine stateMachine)
    {
        float side = enemyTransform.position.x - playerTransform.position.x;
        float height = enemyTransform.position.y - playerTransform.position.y;

        if (MathF.Abs(side) > stateMachine.enemy.aggroRange) {
            stateMachine.followingPath = false;
            stateMachine.unit.StopPathPosition();
            stateMachine.nextState = stateMachine.idle;
            Exit(stateMachine);
        } else if (Vector3.Distance(playerTransform.position, enemyTransform.position) <= stateMachine.enemy.rangedAttackRange) {
            stateMachine.followingPath = false;
            stateMachine.unit.StopPathPosition();
            stateMachine.nextState = stateMachine.attack;
            Exit(stateMachine);
        } else {
            if (!stateMachine.followingPath) {
                stateMachine.followingPath = true;
                stateMachine.unit.StartPath(playerTransform.position);
            }
        }
    }

    public override void Exit(StateMachine stateMachine)
    {
        stateMachine.TransitionState(stateMachine.nextState);
    }
}
