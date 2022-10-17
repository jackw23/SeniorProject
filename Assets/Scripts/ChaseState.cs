using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{

    Transform enemyTransform;
    Transform playerTransform;
    public float movementSpeed = 1.0f;
    // Start is called before the first frame update
    public override void Enter(StateMachine stateMachine) 
    {
        if (enemyTransform == null) 
        {
            enemyTransform = GameObject.FindGameObjectWithTag("GameController").transform;
        }
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public override void Execute(StateMachine stateMachine)
    {
        float side = enemyTransform.position.x - playerTransform.position.x;
        if (side > 3.0f || side < - 3.0f)
        {
            Exit(stateMachine);
        } else if (side > 0) 
        {
            enemyTransform.position = enemyTransform.position - new Vector3(2.0f * movementSpeed * Time.deltaTime, 0, 0);
        } else if (side < 0) 
        {
            enemyTransform.position = enemyTransform.position + new Vector3(2.0f * movementSpeed * Time.deltaTime, 0, 0);
            
        }
    }

    public override void Exit(StateMachine stateMachine)
    {
        stateMachine.TransitionState(stateMachine.moveLeft);
    }
}
