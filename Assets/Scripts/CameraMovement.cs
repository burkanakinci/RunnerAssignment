using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Space]
    [SerializeField] private Vector3 offsetOnPlay;
    [SerializeField] private Transform player;


    [Space]
    [SerializeField] private Vector3 offsetOnFinish;

    private Vector3 lookPos;
    private Quaternion rotation;
    [SerializeField] float rotationLerpValue= 2f;
    [SerializeField] float movementLerpValue = 2f;

    private void LateUpdate()
    {
        MoveCamera();
        LookTarget();
    }
    private void MoveCamera()
    {

        transform.position = Vector3.Lerp(transform.position,
            (PlayerManager.Instance.GetStateMachine().GetCurrentState()==PlayerManager.Instance.GetStateMachine().finishState)?(player.position + offsetOnFinish) :(player.position+offsetOnPlay),
            Time.deltaTime * movementLerpValue);
    }
    private void LookTarget()
    {
        lookPos = player.position - transform.position;
         rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationLerpValue);
    }
    
}