using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 1000;
    int currentHealth;
    public int stageTwoCutOff = 500;
    public int stageThreeCutOff = 200;
    BossStateMachine bossStateMachine;
    BossEnemy bossEnemy;
    // Start is called before the first frame update
    void Start()
    {   
        bossEnemy = GetComponent<BossEnemy>();
        bossStateMachine = GetComponent<BossStateMachine>();

        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount) {
        currentHealth = currentHealth - damageAmount + bossEnemy.damageResistance;

        if (bossEnemy.stageTwo && currentHealth < stageTwoCutOff && !bossEnemy.inStageTwo && !bossEnemy.inStageThree) {
            bossEnemy.speed = bossEnemy.speed * 1.5f;
            bossEnemy.damageResistance = bossEnemy.damageResistance * 2;
            bossStateMachine.TransitionState(bossStateMachine.stageTransition);
            bossEnemy.inStageOne = false;
            bossEnemy.inStageTwo = true;
        }
        if (bossEnemy.stageThree && currentHealth < stageThreeCutOff && !bossEnemy.stageThree) {
            //Change stateMachine states to stage 3 states;
            bossEnemy.inStageThree = true;
            bossEnemy.inStageTwo = false;
            bossEnemy.inStageOne = false;
        }
        if (currentHealth < 0) {
            Death();
        }
    }

    void Death() {
        bossStateMachine.TransitionState(bossStateMachine.death);
    }
}
