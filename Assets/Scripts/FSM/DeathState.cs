using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/State/Death")]
public class DeathState : State
{
    public override void Enter(StateMachine stateMachine)
    {
        Debug.Log("Died");
    }
    public override void Execute(StateMachine stateMachine)
    {
        // Destroy(stateMachine.gameObject);
        stateMachine.boxCollider2D.enabled = false;
        stateMachine.rigidBody.isKinematic = true;
        stateMachine.unit.enabled = false;
        stateMachine.health.enabled = false;
        stateMachine.sprite.enabled = false;
        stateMachine.enabled = false;
    }
    public override void Exit(StateMachine stateMachine)
    {
        throw new NotImplementedException();
    }
}
