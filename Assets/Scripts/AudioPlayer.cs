using UnityEngine;
using UnityEngine.UI;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;

    public Sprite playSprite;
    public Sprite pauseSprite;

    public Image playPauseImage;

    public GameObject playPauseProxy; // invisible 3D cube
    public GameObject resetProxy;

    public CanvasGroup descriptionGroup;

    public AudioClip englishClip;
    public AudioClip frenchClip;

    public LanguageTextLoader languageLoader;

    private bool isVisible = false;
    private bool isTouching = false;
    private bool wasPlaying = false;

    void Start()
    {
        if (audioSource == null || languageLoader == null)
        {
            Debug.LogError("AudioSource or LanguageTextLoader not assigned.");
            return;
        }

        // Set clip based on current language at startup
        UpdateAudioClip();
    }

    void Update()
    {
        isVisible = descriptionGroup.alpha > 0.9f;

        if (!isVisible)
        {
            StopAndReset();
            return;
        }

        // Language change detection
        UpdateAudioClip();

        // Handle end of audio
        if (wasPlaying && !audioSource.isPlaying && audioSource.time >= audioSource.clip.length)
        {
            playPauseImage.sprite = playSprite;
            wasPlaying = false;
        }

        // Reset button visibility logic
        bool canReset = audioSource.clip != null &&
                        audioSource.time > 0f &&
                        audioSource.time < audioSource.clip.length;

        if (resetProxy != null)
            resetProxy.SetActive(canReset);

        // Touch detection for play/pause
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (touch.phase == TouchPhase.Began && hit.transform == playPauseProxy.transform)
                {
                    isTouching = true;
                }
                else if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) && isTouching)
                {
                    isTouching = false;
                    if (hit.transform == playPauseProxy.transform)
                        ToggleAudio();
                }
                else if (touch.phase == TouchPhase.Ended && hit.transform == resetProxy.transform)
                {
                    ResetAudio();
                }
            }
        }
    }

    void ToggleAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            playPauseImage.sprite = playSprite;
        }
        else
        {
            audioSource.Play();
            playPauseImage.sprite = pauseSprite;
            wasPlaying = true;
        }
    }

    void ResetAudio()
    {
        audioSource.Stop();
        audioSource.time = 0f;
        playPauseImage.sprite = playSprite;
        wasPlaying = false;
    }

    public void StopAndReset()
    {
        audioSource.Stop();
        audioSource.time = 0f;
        playPauseImage.sprite = playSprite;
        wasPlaying = false;

        if (resetProxy != null)
            resetProxy.SetActive(false);
    }

    void UpdateAudioClip()
    {
        var lang = languageLoader.selectedLanguage;
        AudioClip targetClip = null;

        switch (lang)
        {
            case LanguageTextLoader.Language.French:
                targetClip = frenchClip;
                break;
            default:
                targetClip = englishClip;
                break;
        }

        if (audioSource.clip != targetClip)
        {
            audioSource.Stop();
            audioSource.clip = targetClip;
            audioSource.time = 0f;
            playPauseImage.sprite = playSprite;
            wasPlaying = false;
        }
    }
}
