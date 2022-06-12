using UnityEngine;

public class DefaultPooledController : MonoBehaviour, IPooledObject
{

    public void OnObjectSpawn(PositionerValue _posValue)
    {
        return;
    }
    public void OnObjectDeactive()
    {
        ObjectPool.Instance.DeactiveObject("Platform",this);

        this.gameObject.SetActive(false);
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
}
