

using System;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class LevelCreator : MonoBehaviour
{
    [HideInInspector] public int createdLevelNumber;
   private LevelData tempLevelData;
    private string savePath;

    [SerializeField]private SplinePositioner[] tempCollactables;
    [SerializeField] private SplinePositioner[] tempObstacles;
    [SerializeField] private SplineComputer tempLevelSpline;

    private PositionerValue tempPositionerValue;
    public void CreateLevel()
    {
#if UNITY_EDITOR
        tempLevelData = ScriptableObject.CreateInstance<LevelData>();
        tempLevelData.collactables = new PositionerValue[tempCollactables.Length];
        tempLevelData.obstacles = new PositionerValue[tempObstacles.Length];
        tempLevelData.obstacleDatas = new ObstacleData[tempObstacles.Length];

        savePath = AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/LevelScriptableObjects/Level" + createdLevelNumber + ".asset");

        tempLevelData.levelSplinePoints= tempLevelSpline.GetPoints();

        for (int i = tempObstacles.Length - 1; i >= 0; i--)
        {
            tempPositionerValue = new PositionerValue();

            tempPositionerValue.percent = tempObstacles[i].GetPercent();
            tempPositionerValue.offset = tempObstacles[i].offsetModifier.keys[0].offset;

            tempLevelData.obstacles[i] = tempPositionerValue;
            tempLevelData.obstacleDatas[i] = tempObstacles[i].GetComponent<ObstacleController>().obstacleData;
        }

        for (int j = tempCollactables.Length - 1;j >= 0; j--)
        {
            tempPositionerValue = new PositionerValue();

            tempPositionerValue.percent = tempCollactables[j].GetPercent() ;
            tempPositionerValue.offset = tempCollactables[j].offsetModifier.keys[0].offset;

            tempLevelData.collactables[j] = tempPositionerValue;
            
        }

        AssetDatabase.CreateAsset(tempLevelData, savePath);
        AssetDatabase.SaveAssets();

#endif
    }

}

