using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRangedAttackState : BossState
{
    float attackTimer, attackAnimationStart, animationTime;
    bool attacked;
    int endLoopCounter = 0;
    Transform rangedPoint;
    BossEnemy bossEnemy;
    Animator animator;
    public override void Enter(BossStateMachine bossStateMachine)
    {
        bossEnemy = bossStateMachine.bossEnemy;
        rangedPoint = bossEnemy.rangedPoint;
        animator = bossStateMachine.animator;
        attacked = false;

        endLoopCounter = endLoopCounter + 1;
        
        if (bossEnemy.bossTwo) {
            AnimationClip[] animationClips = animator.runtimeAnimatorController.animationClips;
            foreach (AnimationClip clip in animationClips) {
                if (clip.name == "Ranged Attack") {
                    attackTimer = clip.length;
                }
            }
        }

        animator.SetBool("idle", true);

        Debug.Log(attackTimer);
        Debug.Log(endLoopCounter);
    }

    public override void Execute(BossStateMachine bossStateMachine)
    {
        if (!attacked) {
            if (bossEnemy.bossTwo) {
                animator.SetBool("ranged", true);
            }
            attackAnimationStart = Time.time;
            animationTime = Time.time + attackTimer;
            attacked = true;
            bossEnemy.RangedAttack();
        }

        if (Time.time > animationTime) {
            animator.SetBool("ranged", false);
        }

        if (bossEnemy.bossTwo && bossEnemy.inStageOne) {
            if (bossStateMachine.previousState == bossStateMachine.rangedAttack) {
                bossStateMachine.nextState = bossStateMachine.chase;
            } else if (bossStateMachine.previousState == bossStateMachine.idle) {
                if (endLoopCounter == 3) {
                    bossStateMachine.nextState = bossStateMachine.idle;
                    bossStateMachine.endLoop = true;
                } else {
                    bossStateMachine.nextState = bossStateMachine.rangedAttack;
                }
            }
        } else if (bossEnemy.bossTwo && bossEnemy.inStageTwo) {
            if (bossStateMachine.previousState == bossStateMachine.meleeAttack) {
                bossStateMachine.nextState = bossStateMachine.meleeAttack;
            } else if (bossStateMachine.previousState == bossStateMachine.rangedAttack) {
                bossStateMachine.nextState = bossStateMachine.idle;
            } //TODO: laser state;
        }

        if (Time.time - attackAnimationStart > bossEnemy.idleRangedTime) {
            Exit(bossStateMachine);
        }
    }

    public override void Exit(BossStateMachine bossStateMachine)
    {
        if (bossStateMachine.endLoop) {
            endLoopCounter = 0;
        }
        bossStateMachine.TransitionState(bossStateMachine.nextState);
    }
}
