using System;
using UnityEngine;
using System.Collections.Generic;
public class LocalizationController : MonoBehaviour
{
    public static Dictionary<string, string> LocalizedText;
    public static Action<string> ChangeLocalization = (loc) =>
    {
        LoadLocalizationFile(loc);
        PlayerPrefs.SetString("Language", loc);
    };

    private void Start()
    {
        ChangeLocalization.Invoke(PlayerPrefs.GetString("Language", "ENG"));
    }
    
    public void SetLocalization(string loc)
    {
        ChangeLocalization.Invoke(loc);
    }
    
    private static void LoadLocalizationFile(string loc)
    {
        LocalizedText = new Dictionary<string, string>();
        var name = "Localization_" + loc;
        var asset = Resources.Load<TextAsset>(name);
        var loadedData = JsonUtility.FromJson<LocalizationData>(asset.ToString());
        foreach (var localizationItem in loadedData.items)
        {
            LocalizedText.Add(localizationItem.key, localizationItem.value);
        }
    }
}
