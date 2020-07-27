using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class EffectsLoader : MonoBehaviour
{
    private AssetBundle bundle;

    private IEnumerable Start()
    {
        using (UnityWebRequest webRequest = UnityWebRequestAssetBundle.GetAssetBundle("https://drive.google.com/uc?export=download&id=1IC1nWbEYsk5-jl203-OGwl1isP4EFAjk"))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.LogError(webRequest.error);
            }
            else
            {
                bundle = DownloadHandlerAssetBundle.GetContent(webRequest);
                Debug.Log($"Bundle {bundle.name} has loaded");
            }
        }
    }

#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha1)){
            var obj = LoadEffect("Effect1");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadEffect("Effect2");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            LoadEffect("Effect3");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            LoadEffect("Effect3");
        }
    }
#endif

    public GameObject LoadEffect(string name)
    {
        var path = $"Effects/{name}";
        var prefab = Resources.Load<GameObject>(path);
        return prefab ? Instantiate(prefab) : null;
    }

    public void LoadEffectFromBundle(string name)
    {
        if (bundle == null)
        {
            bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "effects"));
            Debug.Log("Bundle is loaded from disk");
        }

        if (bundle == null)
        {
            Debug.LogError("Failed to load asset bundle");
            return;
        }

        var prefab = bundle.LoadAsset<GameObject>(name);
        Instantiate(prefab);
        bundle.Unload(false);
    }
    
}