using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementStateMachine : StateMachine
{
    public TapToPlayState tapToPlayState;
    public PlayState playState;
    public FailState failState;
    public FinishState finishState;

    private void Awake()
    {
        tapToPlayState=new TapToPlayState(this);
        playState = new PlayState(this);
        failState = new FailState(this);
        finishState = new FinishState(this);

        GameManager.Instance.levelStart += ChangeInitialState;
    }

    public void ChangeInitialState()
    {
        ChangeState(GetInitialState());
    }

    protected override BaseState GetInitialState()
    {
        return tapToPlayState;
    }
}
