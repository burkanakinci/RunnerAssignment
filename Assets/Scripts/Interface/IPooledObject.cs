using UnityEngine;

public interface IPooledObject
{
    void OnObjectSpawn(PositionerValue _posValue );
    void OnObjectDeactive();
    GameObject GetGameObject();
}
