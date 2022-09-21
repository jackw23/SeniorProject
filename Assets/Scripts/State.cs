using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMS
{
    public abstract class State : ScriptableObject
{
    public abstract void Execute (StateMachine stateMachine);
}
}


