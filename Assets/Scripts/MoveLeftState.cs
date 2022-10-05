using UnityEngine;
using UnityEngine.AI;

public class MoveLeftState : State
{
    public Transform enemyTransform;

    public float movementSpeed = 0.8f;

    public override void Enter(StateMachine stateMachine) 
    {
        if (enemyTransform == null) 
        {
            enemyTransform = GameObject.FindGameObjectWithTag("GameController").transform;
        }
    }

    public override void Execute(StateMachine stateMachine)
    {
        if (enemyTransform.position.x > 1.0f) 
        {
            enemyTransform.position = enemyTransform.position + new Vector3(-1.0f * movementSpeed * Time.deltaTime, 0, 0);
            
        } else 
        {
            Exit(stateMachine);
        }
    }

    public override void Exit(StateMachine stateMachine)
    {
        stateMachine.TransitionState(stateMachine.moveRight);
    }
}
