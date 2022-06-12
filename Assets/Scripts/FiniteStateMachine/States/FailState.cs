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
        PlayerManager.Instance.ChangeSplineFollow(false);
        PlayerManager.Instance.ChangeAnimationIntegerValue(2);
		PlayerManager.Instance.ChangeLineActive(false);

        base.Enter();
    }
        
    public override void UpdateLogic()
    {
        
    }
}
