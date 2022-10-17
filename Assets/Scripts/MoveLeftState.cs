using UnityEngine;
using UnityEngine.AI;
using System;

public class MoveLeftState : State
{
    public Transform enemyTransform;
    public Transform playerTransform;
    public float movementSpeed = 0.8f;

    public override void Enter(StateMachine stateMachine) 
    {
        if (enemyTransform == null) 
        {
            enemyTransform = GameObject.FindGameObjectWithTag("GameController").transform;
        }
        if (playerTransform == null) {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public override void Execute(StateMachine stateMachine)
    {
        if (Math.Abs(playerTransform.position.x - enemyTransform.position.x) < 3.0f) 
        {
            Exit(stateMachine);
        } else if (enemyTransform.position.x > 1.0f) 
        {
            enemyTransform.position = enemyTransform.position + new Vector3(-1.0f * movementSpeed * Time.deltaTime, 0, 0);
            
        } else 
        {
            Exit(stateMachine);
        }
    }

    public override void Exit(StateMachine stateMachine)
    {
        if (Math.Abs(playerTransform.position.x - enemyTransform.position.x) < 3.0f) 
        {
            stateMachine.TransitionState(stateMachine.chase);
        } else 
        {  
            stateMachine.TransitionState(stateMachine.moveRight);
        }
    }
}
