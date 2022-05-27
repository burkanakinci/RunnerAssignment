using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("PlayerManager").AddComponent<PlayerManager>();
            }
            return instance;
        }
    }

    [SerializeField] private MovementStateMachine playerStateMachine;
    [SerializeField] private SplineFollower playerSplineFollower;
    [SerializeField] private Transform basePlayer,floor,characterVisual;

    private Vector3 firstMousePos;
    private float changeOfMousePos, horizontalMovementChange;
    [SerializeField] private float horizontalSpeed = 8f, playerXClampValue = 4.5f;



    private void Awake()
    {
        instance = this;

        playerStateMachine.ChangeState(playerStateMachine.tapToPlayState);
    }

    public void ChangeSplineFollow(bool _isFollow)
    {
        playerSplineFollower.follow = _isFollow;
    }

    public void TouchControlsDown()
    {
       firstMousePos = Input.mousePosition;
    }
    public void TouchControls()
    {

            horizontalMovementChange = 0;

            changeOfMousePos = Input.mousePosition.x - firstMousePos.x;
            if (Mathf.Abs(changeOfMousePos) > 0.1f)
            {

                horizontalMovementChange = (changeOfMousePos * 1 / UIManager.Instance.screenWidth);
                firstMousePos = Input.mousePosition;
            }
    }
    public void TouchControlsUp()
    {
            horizontalMovementChange = 0;
            firstMousePos = Input.mousePosition;
    }

    public void MoveCharacter()
    {
        transform.localPosition = new Vector3(
            Mathf.Clamp(
                Mathf.Lerp(basePlayer.localPosition.x, transform.localPosition.x + (horizontalMovementChange * playerXClampValue * 2f), horizontalSpeed * Time.deltaTime),
                            (-1f * playerXClampValue),
                            playerXClampValue),
                basePlayer.localPosition.y,
                basePlayer.localPosition.z);
    }

    public void ChangeFloorYScale( float changeValue)
    {
        floor.DOScaleY(floor.localScale.y+changeValue,1f);
        characterVisual.DOLocalMoveY(characterVisual.localPosition.y + changeValue,1f);
    }
}
