using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollactableController : MonoBehaviour
{
    [SerializeField] private CollactableData collactableData;

    private void OnEnable()
    {
        gameObject.layer = LayerMask.NameToLayer("Collactable");
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.layer = LayerMask.NameToLayer("Default");

        PlayerManager.Instance.ChangeFloorYScale(collactableData.raiseValue);
    }
}
