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
        
        PlayerManager.Instance.ChangeSplineFollow(false);
		PlayerManager.Instance.ChangeLineActive(false);
		
        PlayerManager.Instance.JumpFinishPlatform();
        PlayerManager.Instance.ChangeAnimationIntegerValue(1);
        GameManager.Instance.SetVCamOnFinish();

        PlayerManager.Instance.SetPlayerCoin();
        UIManager.Instance.ShowPlayerCoin();
        base.Enter();
    }
        
    public override void UpdateLogic()
    {
        
    }
}
