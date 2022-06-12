using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dreamteck.Splines;

public class ObstacleController : MonoBehaviour, IPooledObject
{
    public ObstacleData obstacleData;
    [SerializeField] private TextMeshPro obstacleText;
    [SerializeField] private MeshRenderer obstacleRenderer;

    [SerializeField] private SplinePositioner obstaclePositioner;

    public void OnObjectSpawn(PositionerValue _posValue)
    {
        obstaclePositioner.spline = GameManager.Instance.levelSpline;

        obstaclePositioner.SetPercent(_posValue.percent);
        obstaclePositioner.offsetModifier.keys[0].offset = _posValue.offset;

        obstaclePositioner.Rebuild();

        obstacleData = GameManager.Instance.GetObstacleData(_posValue);
        obstacleText.text = "-" + obstacleData.downgradeValue;
        obstacleRenderer.material = obstacleData.obstacleMaterial;

        transform.localScale = Vector3.one * (obstacleData.downgradeValue * 2f);

        return;
    }
    public void OnObjectDeactive()
    {
        ObjectPool.Instance.DeactiveObject("Obstacle", this);

        this.gameObject.SetActive(false);
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        gameObject.layer = LayerMask.NameToLayer("Default");

        PlayerManager.Instance.ChangeFloorYScale((-1f* obstacleData.downgradeValue));
    }
}
