using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToPlayState : BaseState
{

    private MovementStateMachine movementStateMachine;

    public TapToPlayState(MovementStateMachine stateMachine) : base("TapToPlayState", stateMachine)
    {
        movementStateMachine = (MovementStateMachine)stateMachine;
    }

    public override void Enter()
    {
        PlayerManager.Instance.ChangeSplineFollow(false);
    }
        
    public override void UpdateLogic()
    {
        if (Input.GetMouseButtonUp(0))
        {
            movementStateMachine.ChangeState(movementStateMachine.playState);
        }
    }
}
