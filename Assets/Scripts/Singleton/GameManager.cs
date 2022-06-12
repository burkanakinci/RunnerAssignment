using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Dreamteck.Splines;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int level
    {
        get
        {
            if (!PlayerPrefs.HasKey("Level"))
            {
                PlayerPrefs.SetInt("Level", 1);
            }

            return PlayerPrefs.GetInt("Level");

        }

        set => PlayerPrefs.SetInt("Level", value);
    }

    public event Action levelStart;
    public LevelData levelData;
    public SplineComputer levelSpline;
    public SplineMesh[] splineMeshes;

    private int maxLevelObject = 1;
    private int tempLevel;

    public SplinePositioner  finishPlatform;

    public bool fpsLock = true;

    public ParticleSystem finishConfettiParticle;

    [SerializeField] private GameObject onPlayVCam, onFinishVCam;

    private void Awake()
    {
        Instance = this;

        if (fpsLock)
        {
            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = 60;
        }

        levelStart += CleanSceneObject;
        levelStart += EditSpline;
        
        levelStart += SpawnSceneObject;

        
    }
    private void Start()
    {
        maxLevelObject = Resources.LoadAll("LevelScriptableObjects", typeof(LevelData)).Length;

        levelStart += PlayerManager.Instance.ResetPlayerTransforms;

        StartLevelStartAction();
    }

    public void StartLevelStartAction()
    {
        levelStart?.Invoke();
    }

    private void CleanSceneObject()
    {
        ObjectPool.Instance.SceneObjectToPool();
    }
    private void EditSpline()
    {
        GetLevelData();

        levelSpline.SetPoints(levelData.levelSplinePoints);

        levelSpline.Rebuild();

        for (int k=splineMeshes.Length-1;k>=0;k--)
        {
            splineMeshes[k].GetChannel(0).count = levelData.levelSplinePoints.Length * 100;
            splineMeshes[k].Rebuild();
        }

        finishPlatform.Rebuild();
    }
    private void SpawnSceneObject()
    {

        for (int i = levelData.obstacles.Length - 1; i >= 0; i--)
        {
            ObjectPool.Instance.SpawnFromPool("Obstacle", levelData.obstacles[i]);
        }
        for (int j = levelData.collactables.Length - 1; j>= 0; j--)
        {
            ObjectPool.Instance.SpawnFromPool("Collactable", levelData.collactables[j]);
        }
    }
    public ObstacleData GetObstacleData(PositionerValue _pos)
    {
        return levelData.obstacleDatas[Array.FindIndex(levelData.obstacles, _x =>_x == _pos)];
    }
    private void GetLevelData()
    {
        tempLevel = (level % maxLevelObject) > 0 ? (level % maxLevelObject) : maxLevelObject;

        levelData = Resources.Load<LevelData>("LevelScriptableObjects/Level" + tempLevel);
    }

    public void SetVCamOnPlay()
    {
        onPlayVCam.SetActive(true);
        onFinishVCam.SetActive(false);
    }
    public void SetVCamOnFinish()
    {
        onPlayVCam.SetActive(false);
        onFinishVCam.SetActive(true);
    }
}
