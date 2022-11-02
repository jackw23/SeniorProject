using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100.0f;
    public float currentHealth;
    StateMachine stateMachine;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        stateMachine = GetComponent<StateMachine>();
    }

    public void TakeDamage(float amount) {
        currentHealth = currentHealth - amount;
        //Animator.TakeDamage;
        
        if (currentHealth <= 0) {
            Death();
        }
    }

    private void Death() {
        stateMachine.TransitionState(stateMachine.death);
    }
}
