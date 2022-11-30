using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMeleeAttackState : BossState
{
    Transform attackPoint;
    BossEnemy bossEnemy;
    Animator animator;
    float nextAttackTime = 0f, attackAnimation = 5.0f, attackAnimationStart, attackTimer, animationTime;
    bool attacked;

    public override void Enter(BossStateMachine bossStateMachine)
    {
        bossEnemy = bossStateMachine.bossEnemy;
        attackPoint = bossEnemy.meleePointOne;
        attacked = false;
        animator = bossStateMachine.animator;
        animator.SetBool("idle", true);

        if (bossEnemy.bossTwo) {
            AnimationClip[] animationClips = animator.runtimeAnimatorController.animationClips;
            foreach (AnimationClip clip in animationClips) {
                if (clip.name == "Melee") {
                    attackTimer = clip.length;
                }
            }
        }
        
        Debug.Log("Entering attack state");

    }

    public override void Execute(BossStateMachine bossStateMachine)
    {

        if (!attacked) {
            if (bossEnemy.bossTwo) {
                animator.SetBool("melee", true);
            } 
            attackAnimationStart = Time.time;
            animationTime = Time.time + attackTimer;
            attacked = true;
            bossEnemy.MeleeAttack(false, false); //TODO: Change melee attack helper
        }

        if (Time.time >= animationTime) {
            if (bossEnemy.bossTwo) {
                animator.SetBool("melee", false);
            }

        }
        
        if (bossEnemy.bossTwo && bossEnemy.inStageOne) {
            if (bossStateMachine.previousState == bossStateMachine.chase) {
                bossStateMachine.nextState = bossStateMachine.meleeAttack;
            } else if (bossStateMachine.previousState == bossStateMachine.meleeAttack) {
                bossStateMachine.nextState = bossStateMachine.chase;
            }
        } else if (bossEnemy.bossTwo && bossEnemy.inStageTwo) {
            if (bossStateMachine.previousState == bossStateMachine.chase) {
                bossStateMachine.nextState = bossStateMachine.rangedAttack;
            } else if (bossStateMachine.previousState == bossStateMachine.rangedAttack) {
                bossStateMachine.nextState = bossStateMachine.chase;
            }
        }


        if (Time.time - attackAnimationStart > bossEnemy.idleMeleeTime) {
            Exit(bossStateMachine);
        }

    }

    public override void Exit(BossStateMachine bossStateMachine)
    {
        animator.SetBool("idle", true);
        animator.SetBool("attack1", false);
        bossStateMachine.TransitionState(bossStateMachine.nextState);
    }
}
