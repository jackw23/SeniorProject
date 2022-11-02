using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "FSM/State/Idle")]
public class IdleState : State
{
    private float time;
    private Transform playerTransform, enemyTransform;
    public override void Enter(StateMachine stateMachine)
    {
        time = Time.time;
        playerTransform = stateMachine.playerTransform;
        enemyTransform = stateMachine.transform;
        //Animator.setTrigger("Idle");
    }

    public override void Execute(StateMachine stateMachine)
    {
        float horizontalDistance = Math.Abs(enemyTransform.position.x - playerTransform.position.x);

        if (horizontalDistance < 3.0f && stateMachine.enemy.mobile && stateMachine.enemy.aggressive) {
            stateMachine.nextState = stateMachine.chase;
            Exit(stateMachine);
        }

        if (Time.time - time > stateMachine.enemy.idleTime) {
            if (stateMachine.enemy.mobile) {
                if (stateMachine.enemy.verticalMovement && stateMachine.unit.flying) {
                    if (enemyTransform.position.y > stateMachine.GetOriginalPosition().y) {
                        stateMachine.nextState = stateMachine.moveDown;
                    } else {
                        stateMachine.nextState = stateMachine.moveUp;
                    }
                    Exit(stateMachine);
                } else {
                    if (enemyTransform.position.x > stateMachine.GetOriginalPosition().x) {
                        stateMachine.nextState = stateMachine.moveLeft;
                    } else {
                        stateMachine.nextState = stateMachine.moveRight;
                    }
                    Exit(stateMachine);
                }
            } else if (stateMachine.enemy.constantAim) {
                stateMachine.nextState = stateMachine.attack;
                Exit(stateMachine);
            } else if (stateMachine.enemy.constantMelee) {
                stateMachine.nextState = stateMachine.attack;
                Exit(stateMachine);
            }
            
        }

        if (Vector3.Distance(enemyTransform.position, playerTransform.position) <= 1.5f && stateMachine.enemy.aggressive) {
            stateMachine.nextState = stateMachine.attack;
            Exit(stateMachine);
        }
    }

    public override void Exit(StateMachine stateMachine)
    {
        stateMachine.TransitionState(stateMachine.nextState);
    }
}
