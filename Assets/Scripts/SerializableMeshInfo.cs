using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableMeshInfo
{
    public string name;
    public Material material;

    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uv;
    public Vector2[] uv2;
    public Vector3[] normals;
    public Color[] colors;

    public SerializableMeshInfo(string name, Mesh mesh, Material material)
    {
        this.name = name;
        this.material = material;

        vertices = mesh.vertices;
        triangles = mesh.triangles;
        uv = mesh.uv;
        uv2 = mesh.uv2;
        normals = mesh.normals;
        colors = mesh.colors;
    }

    public GameObject BuildObject(Transform parent = null)
    {
        var obj = new GameObject($"{name} copy");
        var meshFilter = obj.AddComponent<MeshFilter>();
        var meshRenderer = obj.AddComponent<MeshRenderer>();
        
        meshFilter.sharedMesh = GetMesh();
        meshRenderer.sharedMaterial = material;

        if (parent != null)
        {
            obj.transform.parent = parent;
        }

        return obj;
    }

    private Mesh GetMesh()
    {
        var mesh = new Mesh()
        {
            vertices = vertices,
            triangles = triangles,
            uv = uv,
            uv2 = uv2,
            normals = normals,
            colors = colors
        };

        return mesh;
    }
}
