using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FMS
{
    public class StateMachine : MonoBehaviour
{   

    public State currentState;

    [SerializeField] private State initialState;
    
    void Awake()
    {
        currentState = initialState;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Execute(this);
    }

    // TODO: Add components needed by the grunt for its various states, such as initial position and whatnot
}
}

