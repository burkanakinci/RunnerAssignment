using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishState : BaseState
{

    private MovementStateMachine movementStateMachine;

    public FinishState(MovementStateMachine stateMachine) : base("FinishState", stateMachine)
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
