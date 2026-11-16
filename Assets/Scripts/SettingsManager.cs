using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public CanvasGroup settingsGroup;
    public TMP_Dropdown languageDropdown;
    public LanguageTextLoader textLoader; // <-- Reference to your language loader

    private bool isVisible = false;

    void Start()
    {
        if (settingsGroup != null)
        {
            settingsGroup.alpha = 0f;
            settingsGroup.interactable = false;
            settingsGroup.blocksRaycasts = false;
        }

        if (languageDropdown != null)
        {
            languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
        }

        // Optionally: Set dropdown based on saved language
        string currentLang = PlayerPrefs.GetString("Language", "en");
        languageDropdown.value = (currentLang == "fr") ? 1 : 0;
    }

    public void ToggleSettings()
    {
        isVisible = !isVisible;
        settingsGroup.alpha = isVisible ? 1f : 0f;
        settingsGroup.interactable = isVisible;
        settingsGroup.blocksRaycasts = isVisible;
    }

    void OnLanguageChanged(int index)
    {
        string selectedLang = (index == 0) ? "en" : "fr";
        PlayerPrefs.SetString("Language", selectedLang);
        PlayerPrefs.Save();

        if (textLoader != null)
        {
            textLoader.SetLanguage(selectedLang);
        }

        Debug.Log("Language changed to: " + selectedLang);
    }
}
