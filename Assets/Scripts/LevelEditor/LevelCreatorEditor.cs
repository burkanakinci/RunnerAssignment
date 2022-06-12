using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomEditor(typeof(LevelCreator))]
public class LevelCreatorEditor : Editor
{
#if UNITY_EDITOR
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelCreator levelCreator = (LevelCreator)target;

        GUILayout.Space(25f);

        EditorGUI.BeginChangeCheck();

        GUILayout.Label("Number Of Level To Be Created");
        levelCreator.createdLevelNumber = EditorGUILayout.IntField("Number Of Level", levelCreator.createdLevelNumber);

        GUILayout.Space(25f);

        if (GUILayout.Button("CreateLevel"))
        {
            levelCreator.CreateLevel();
        }
    }
#endif
}

