using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateAssetBundle 
{
    [MenuItem(("Asset Bundles/Build"))]
    private static void BuildAllAssetBundles()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetsBundles/", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        AssetDatabase.Refresh();
    }

    [MenuItem(("Asset Bundles/Get Names"))]
    private static void GetName()
    {
        var names = AssetDatabase.GetAllAssetBundleNames();
        foreach (var name in names)
        {
            Debug.Log($"Asset bundle name : {name}");
        }
    }
}
