using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossState
{
    Transform playerTransform, enemyTransform;
    BossEnemy bossEnemy;
    Animator animator;
    float time;
    public override void Enter(BossStateMachine bossStateMachine)
    {
        playerTransform = bossStateMachine.playerTransform;
        enemyTransform = bossStateMachine.transform;
        bossEnemy = bossStateMachine.bossEnemy;
        animator = bossStateMachine.animator;
        animator.SetBool("idle", true);
        time = Time.time;

        if (bossStateMachine.transform.position.x - bossStateMachine.playerTransform.position.x > 0) {
            bossStateMachine.transform.rotation = Quaternion.LookRotation(Vector3.back);
        } else {
            bossStateMachine.transform.rotation = Quaternion.LookRotation(Vector3.forward);
        }
    }

    public override void Execute(BossStateMachine bossStateMachine)
    {   
        float distance = Mathf.Abs(playerTransform.position.x - enemyTransform.position.x);
        
        if (!bossEnemy.fightStarted) {
            bossStateMachine.nextState = bossStateMachine.fightStart;
            if (Time.time - time > bossEnemy.idleStartUpTime) {
                Exit(bossStateMachine);
            }
        } else if (bossEnemy.bossOne){
            if (distance < bossEnemy.meleeAttackRange) {
                bossStateMachine.nextState = bossStateMachine.meleeUp;
                Exit(bossStateMachine);
            }
            if (Time.time - time > bossEnemy.idleTime) {
                if (bossStateMachine.previousState == bossStateMachine.fightStart) {
                    bossStateMachine.nextState = bossStateMachine.chase;
                } else if (bossStateMachine.previousState == bossStateMachine.jumpAway && !bossStateMachine.endLoop) {
                    bossStateMachine.nextState = bossStateMachine.jump;
                } else if (bossStateMachine.previousState == bossStateMachine.jumpAway && bossStateMachine.endLoop) {
                    bossStateMachine.nextState = bossStateMachine.chase;
                }
                Exit(bossStateMachine);
            } 
        } else if (bossEnemy.bossTwo && bossEnemy.inStageOne) {
            if (bossStateMachine.previousState == bossStateMachine.fightStart) {
                bossStateMachine.nextState = bossStateMachine.chase;
            } else if (bossStateMachine.previousState == bossStateMachine.rangedAttack && bossStateMachine.endLoop) {
                bossStateMachine.nextState = bossStateMachine.chase;
            } else if (bossStateMachine.previousState == bossStateMachine.chase) {
                bossStateMachine.nextState = bossStateMachine.rangedAttack;
            }
            if (Time.time - time > bossEnemy.idleTime) {
                Exit(bossStateMachine);
            }
        } else if (bossEnemy.bossTwo && bossEnemy.inStageTwo) {
            if (bossStateMachine.previousState == bossStateMachine.stageTransition) {
                bossStateMachine.nextState = bossStateMachine.chase;
            } else if (bossStateMachine.previousState == bossStateMachine.rangedAttack) {
                bossStateMachine.nextState = bossStateMachine.chase;
            }
        }
    }

    public override void Exit(BossStateMachine bossStateMachine)
    {
        animator.SetBool("idle", false);
        bossStateMachine.TransitionState(bossStateMachine.nextState);
    }
}
