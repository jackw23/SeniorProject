using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMS
{
    public class AttackIdleTransition : StateTransition
    {
        public override void Execute (StateMachine stateMachine) {
            if (stateMachine.currentState == state1)
            {
                //state 1 is idle
                //check if player enters attack range
            } 
            else if (stateMachine.currentState == state2)
            {
                //state 2 is attack
                //check if player exits attack range
            }
        }
    }
}

