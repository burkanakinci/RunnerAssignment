using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayState : BaseState
{

    private MovementStateMachine movementStateMachine;

    public PlayState(MovementStateMachine stateMachine) : base("PlayState", stateMachine)
    {
        movementStateMachine = (MovementStateMachine)stateMachine;
    }

    public override void Enter()
    {
        PlayerManager.Instance.ChangeSplineFollow(true);
    }
        
    public override void UpdateLogic()
    {

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            PlayerManager.Instance.TouchControlsDown();
        }
        else if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            PlayerManager.Instance.TouchControls();
        }
        else if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            PlayerManager.Instance.TouchControlsUp() ;
        }

        PlayerManager.Instance.MoveCharacter();

        if(Input.GetKey(KeyCode.J))
        {
            PlayerManager.Instance.ChangeFloorYScale(0.5f);
        }
    }
}