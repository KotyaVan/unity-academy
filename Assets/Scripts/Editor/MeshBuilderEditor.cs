using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MeshBuilderEditor : EditorWindow
{
    private static SerializableMeshInfo meshInfo;
    private const string LevelDataPath = "Assets/Data/LevelsData/";

    [MenuItem("Tools/Mesh/Save mesh")]
    private static void SaveSelectedMesh()
    {
        Debug.Log("Log!");

        var obj = Selection.activeGameObject;
        if (obj == null)
        {
            Debug.LogError("No found game object");
            return;
        }

        var meshFilter = obj.GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogError("Not found mesh filter in selected game object");
        }

        var meshRenderer = obj.GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            Debug.LogError("Not found mesh renderer in selected game object");
            return;
        }

        meshInfo = new SerializableMeshInfo(obj.name, meshFilter.sharedMesh, meshRenderer.sharedMaterial);

        PlayerPrefs.SetString("MeshInfo", JsonUtility.ToJson(meshInfo));
    }

    [MenuItem("Tools/Mesh/Load mesh")]
    private static void LoadMesh()
    {
        if (meshInfo == null && PlayerPrefs.HasKey("MeshInfo"))
        {
            Debug.Log("Loaded from Player Prefs");
            meshInfo = JsonUtility.FromJson<SerializableMeshInfo>(PlayerPrefs.GetString("MeshInfo"));
        }

        if (meshInfo == null)
        {
            Debug.LogError("No mesh in memory");
            return;
        }

        var obj = Selection.activeGameObject;
        meshInfo.BuildObject(obj != null ? obj.transform : null);
    }

    [MenuItem("Tools/Mesh/Save to data")]
    private static void SaveSelectedToData()
    {
        var obj = Selection.activeGameObject;
        if (obj == null)
        {
            Debug.LogError("No object in selection");
            return;
        }

        var data = CreateAsset<LevelMeshesData>("Level_test_");
        data.SetupData(obj);
    }

    private static T CreateAsset<T>(string name = "") where T : ScriptableObject
    {
        var asset = CreateInstance<T>();
        var pathAndName = AssetDatabase.GenerateUniqueAssetPath($"{LevelDataPath}{name}{typeof(T)}.asset");
        AssetDatabase.CreateAsset(asset, pathAndName);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
        return asset;
    }

    [MenuItem("Tools/Mesh/Load from data")]
    private static void LoadMeshesFromData()
    {
        var name = "Level_test_";
        var pathAndName = $"{LevelDataPath}{name}{typeof(LevelMeshesData)}.asset";
        var data = AssetDatabase.LoadAssetAtPath(pathAndName, typeof(LevelMeshesData)) as LevelMeshesData;

        if (data == null)
        {
            Debug.LogError($"Data is not found for {pathAndName}");
            return;
        }

        var obj = Selection.activeGameObject;
        foreach (var meshInfo in data.MeshesInfo)
        {
            meshInfo.BuildObject(obj ? obj.transform : null);
        }
    }

    [MenuItem("Tools/Mesh/Add noise to selected mesh")]
    private static void AddNoise()
    {
        var obj = Selection.activeGameObject;
        if (obj == null)
        {
            return;
        }

        var meshFilter = obj.GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            return;
        }

        var vertices = meshFilter.sharedMesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            var pos = vertices[i];
            pos.x += Random.Range(-0.1f, 0.1f);
            pos.y += Random.Range(-0.1f, 0.1f);
            pos.z += Random.Range(-0.1f, 0.1f);
        }

        meshFilter.sharedMesh.vertices = vertices;
    }

    [MenuItem("Tools/Mesh/Create Triangles")]
    private static void CreateTriangles()
    {
        var obj = Selection.activeGameObject;
        if (obj == null)
        {
            obj = new GameObject("Triangle object");
        }

        var meshFilter = obj.GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            meshFilter = obj.AddComponent<MeshFilter>();
        }
        
        var meshRenderer = obj.GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            meshRenderer = obj.AddComponent<MeshRenderer>();
        }

        var vertices = new Vector3[4];
        vertices[0] = new Vector3(0,0,0);
        vertices[1] = new Vector3(0,0,1);
        vertices[2] = new Vector3(1,0,0);
        vertices[3] = new Vector3(1,0,1);
        
        var uv = new Vector2[4];
        uv[0] = new Vector2(0,0);
        uv[1] = new Vector2(0,1);
        uv[2] = new Vector2(1,0);
        uv[3] = new Vector2(1,1);

        var triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        
        triangles[3] = 1;
        triangles[4] = 3;
        triangles[5] = 2;

        var mesh = new Mesh
        {
            vertices = vertices,
            triangles = triangles,
            uv = uv
        };

        meshFilter.sharedMesh = mesh;
        meshFilter.sharedMesh.RecalculateBounds();
    }

    
    
}