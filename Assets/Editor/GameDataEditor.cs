using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GameDataEditor : EditorWindow
{
    public GameData GameData;
    private string gameDataFilePath = "/Data/GameData.json";

    [MenuItem("Tools/Game Data Editor")]
    private static void Init()
    {
        GetWindow(typeof(GameDataEditor)).Show();
    }

    private void OnGUI()
    {
        if (GameData != null)
        {
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("GameData");
            EditorGUILayout.PropertyField(serializedProperty, true);
            serializedObject.ApplyModifiedProperties();
            
            if (GUILayout.Button("Save Data"))
            {
                SaveGameData();
            }
        }

        if (GUILayout.Button("Load Data"))
        {
            LoadGameData();
        }
    }

    private void SaveGameData()
    {
        var dataAsJson = JsonUtility.ToJson(GameData);
        var path = Application.dataPath + gameDataFilePath;
        File.WriteAllText(path,dataAsJson);
        AssetDatabase.Refresh();
    }

    private void LoadGameData()
    {
        var path = Application.dataPath + gameDataFilePath;
        if (File.Exists(path))
        {
            var dataAsJson = File.ReadAllText(path);
            GameData = JsonUtility.FromJson<GameData>(dataAsJson);
        }
        else
        {
            GameData = new GameData();
        }
    }
}
