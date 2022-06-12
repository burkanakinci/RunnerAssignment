using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class CollactableController : MonoBehaviour,IPooledObject
{
    [SerializeField] private CollactableData collactableData;
    [SerializeField] private SplinePositioner collactablePositioner;

    public void OnObjectSpawn(PositionerValue _posValue)
    {
        collactablePositioner.spline = GameManager.Instance.levelSpline;

        collactablePositioner.SetPercent(_posValue.percent);
        collactablePositioner.offsetModifier.keys[0].offset = _posValue.offset;

        collactablePositioner.Rebuild();

        return;
    }
    public void OnObjectDeactive()
    {
        ObjectPool.Instance.DeactiveObject("Collactable", this);

        this.gameObject.SetActive(false);
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.layer = LayerMask.NameToLayer("Default");

        PlayerManager.Instance.ChangeFloorYScale(collactableData.raiseValue);

        OnObjectDeactive();
    }
}
