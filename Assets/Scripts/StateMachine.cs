using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{   

    public State currentState;
    public MoveRightState moveRight;
    public MoveLeftState moveLeft;
    
    //[SerializeField] private State initialState;

    void Start()
    {
        moveRight = (MoveRightState)ScriptableObject.CreateInstance(typeof(MoveRightState));
        moveLeft = (MoveLeftState)ScriptableObject.CreateInstance(typeof(MoveLeftState));
        currentState = moveRight;
        currentState.Enter(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Execute(this);
    }

    public void TransitionState(State state) 
    {
        currentState = state;
        currentState.Enter(this);
    }
    // TODO: Add components needed by the grunt for its various states, such as initial position and whatnot
}


