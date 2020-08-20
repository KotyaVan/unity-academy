using UnityEngine;
using UnityEngine.UI;
public class Localization : MonoBehaviour
{
    private Text text;
    private string key;
    private void Awake()
    {
        text = GetComponent<Text>();
        key = text.text;
        LocalizationController.ChangeLocalization += ChangeLocalization;
    }

    private void OnDestroy()
    {
        LocalizationController.ChangeLocalization -= ChangeLocalization;
    }

    private void ChangeLocalization(string loc)
    {
        text.text = LocalizationController.LocalizedText[key];
    }
}