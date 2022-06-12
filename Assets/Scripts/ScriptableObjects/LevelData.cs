using UnityEngine;
using Dreamteck.Splines;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    public PositionerValue[] obstacles;
    public ObstacleData[] obstacleDatas;
    public PositionerValue[] collactables;
    public SplinePoint[] levelSplinePoints;
}
[Serializable]
public class PositionerValue
{
    public double percent;
    public Vector2 offset;
}
