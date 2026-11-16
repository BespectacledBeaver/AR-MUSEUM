using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanguageTextLoader : MonoBehaviour
{
    public enum Language
    {
        English,
        French
    }

    [Header("UI Texts to Populate")]
    public List<TextMeshProUGUI> textFields;

    [Header("Language Selection")]
    public Language selectedLanguage = Language.English;

    private Language lastLoadedLanguage;

    private const string Separator = "-----------------------------------------";

    void Start()
    {
        lastLoadedLanguage = selectedLanguage;
        LoadLanguageTexts();
    }

    void Update()
    {
        if (selectedLanguage != lastLoadedLanguage)
        {
            LoadLanguageTexts();
            lastLoadedLanguage = selectedLanguage;
        }
    }

    /// <summary>
    /// Call this from SettingsManager or elsewhere to change language dynamically.
    /// </summary>
    public void SetLanguage(string langCode)
    {
        switch (langCode.ToLower())
        {
            case "fr":
                selectedLanguage = Language.French;
                break;
            default:
                selectedLanguage = Language.English;
                break;
        }

        LoadLanguageTexts();
    }

    void LoadLanguageTexts()
    {
        string fileName = GetFileName(selectedLanguage);
        TextAsset langFile = Resources.Load<TextAsset>($"Languages/{fileName}");

        if (langFile == null)
        {
            Debug.LogError($"Language file '{fileName}' not found in Resources/Languages/");
            return;
        }

        string[] sections = langFile.text.Split(
            new[] { Separator },
            System.StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < textFields.Count; i++)
        {
            if (i < sections.Length)
            {
                textFields[i].text = sections[i].Trim();
            }
            else
            {
                textFields[i].text = "[Missing Translation]";
            }
        }
    }

    string GetFileName(Language lang)
    {
        return lang switch
        {
            Language.French => "fr",
            _ => "en"
        };
    }
}
