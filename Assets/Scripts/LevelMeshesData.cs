using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Data", menuName = "Levels/Data", order = 1)]
public class LevelMeshesData : ScriptableObject
{
    [FormerlySerializedAs("meshInfo")] [SerializeField] private SerializableMeshInfo[] meshesInfo;
    public SerializableMeshInfo[] MeshesInfo => meshesInfo;

    public void SetupData(GameObject obj)
    {
        var data = new List<SerializableMeshInfo>();
        foreach (var renderer in obj.transform.GetComponentsInChildren<MeshRenderer>())
        {
            var shadedMaterial = renderer.sharedMaterial;
            var target = renderer.gameObject;
            var sharedMesh = target.GetComponent<MeshFilter>().sharedMesh;

            var meshInfo = new SerializableMeshInfo(obj.name, sharedMesh, shadedMaterial);
            data.Add(meshInfo);
        }

        meshesInfo = data.ToArray();
    }
}
