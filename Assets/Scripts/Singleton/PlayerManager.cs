using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using DG.Tweening;
using System;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField] private MovementStateMachine playerStateMachine;
    [SerializeField] private SplineFollower playerSplineFollower;
    private float speed;
    private bool isSpeedNormal;
    [SerializeField] private float speedUpMultiplier;
    [SerializeField] private Transform basePlayer,floor,characterVisual;

    private Vector3 firstMousePos;
    private float changeOfMousePos, horizontalMovementChange;
    [SerializeField] private float horizontalSpeed = 8f, playerXClampValue = 4.5f;

    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator speedUpImageAnimator;

    [SerializeField] private Transform finishPoint;

    [SerializeField] private Transform line;
    public int playerCoin
    {
        get
        {
            if (!PlayerPrefs.HasKey("PlayerCoin"))
            {
                PlayerPrefs.SetInt("PlayerCoin", 0);
            }

            return PlayerPrefs.GetInt("PlayerCoin");

        }

        set => PlayerPrefs.SetInt("PlayerCoin", value);
    }

    private float targetY;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        targetY = floor.localScale.y;

         speed= playerSplineFollower.followSpeed;
        isSpeedNormal = true;
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

                horizontalMovementChange = (changeOfMousePos * 1 / Screen.width);
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
        targetY += changeValue;

        floor.DOScaleY(targetY,0.65f).OnComplete(()=>ControlFloorYScale());
        characterVisual.DOLocalMoveY((targetY-0.5f),0.65f);
    }
    private void ControlFloorYScale()
    {
        if(floor.localScale.y<=0.1f)
        {
            PlayerManager.Instance.playerStateMachine.ChangeState(PlayerManager.Instance.playerStateMachine.failState);
        }
    }

    public void PlayIdleAnimation()
    {
        playerAnimator.Play("Idle", 0, 0.0f);
        playerAnimator.SetBool("Skate", false);
        playerAnimator.SetInteger("FailOrJump", -1);
    }
    public void PlaySkateAnimation()
    {
        playerAnimator.SetBool("Skate",true);
    }
    public void ChangeAnimationIntegerValue(int _value)
    {
        playerAnimator.SetInteger("FailOrJump",_value);
    }

    public MovementStateMachine GetStateMachine()
    {
        return playerStateMachine;
    }

    public void SpeedUp()
    {
        if(isSpeedNormal)
        {
            speedUpImageAnimator.Play("SpeedUpImage");
            playerSplineFollower.followSpeed *= speedUpMultiplier;
            isSpeedNormal = false;
            StartCoroutine(NormalSpeed());
        }
        
    }
    public IEnumerator NormalSpeed()
    {
        yield return new WaitForSeconds(5f);
        speedUpImageAnimator.Play("Empty");
        playerSplineFollower.followSpeed = speed;
        isSpeedNormal = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Finish"))
        {
            playerStateMachine.ChangeState(playerStateMachine.finishState);
        }
    }

    public void JumpFinishPlatform()
    {
        characterVisual.DOJump(finishPoint.position,1f,1,2f).OnComplete(()=>JumpComplete());
    }
    private void JumpComplete()
    {
        GameManager.Instance.finishConfettiParticle.Play();
        StartCoroutine(UIManager.Instance.ShowSuccessPanel());
    }
    public void ResetPlayerTransforms()
    {
        characterVisual.localPosition = Vector3.up * 0.5f;

        floor.localScale = Vector3.one;
        targetY = floor.localScale.y;
        playerSplineFollower.SetPercent(0.0);

    }

    public void SetPlayerCoin()
    {
        playerCoin += ((int)(floor.localScale.y * 10));
    }
    public void ChangeLineActive(bool _isActive)
    {
        line.gameObject.SetActive(_isActive);
    }
}
