using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMS
{
    public abstract class StateTransition : ScriptableObject
    {
        public State state1;
        public State state2;
        
        public abstract void Execute(StateMachine stateMachine);
    }
}

