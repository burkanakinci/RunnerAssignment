using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


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
        PlayerManager.Instance.PlayIdleAnimation();

        GameManager.Instance.SetVCamOnPlay();

        base.Enter();
    }
        
    public override void UpdateLogic()
    {
        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            PlayerManager.Instance.ChangeLineActive(true);
            movementStateMachine.ChangeState(movementStateMachine.playState);
        }
    }
}
