using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailState : BaseState
{

    private MovementStateMachine movementStateMachine;

    public FailState(MovementStateMachine stateMachine) : base("FailState", stateMachine)
    {
        movementStateMachine = (MovementStateMachine)stateMachine;
    }

    public override void Enter()
    {

    }
        
    public override void UpdateLogic()
    {
        
    }
}
