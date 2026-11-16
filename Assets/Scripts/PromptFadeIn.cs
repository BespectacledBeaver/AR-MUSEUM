using System.Collections;
using UnityEngine;
using Vuforia;

public class DescriptionFadeIn : MonoBehaviour
{
    public CanvasGroup descriptionCanvas;
    public float fadeDuration = 1f;
    public float triggerDistance = 0.5f;

    private ObserverBehaviour observerBehaviour;
    private bool isTracking = false;
    private bool isFading = false;
    private bool isVisible = false;

    private float initialDelay = 5f;
    private bool delayFinished = false;

    void Start()
    {
        observerBehaviour = GetComponent<ObserverBehaviour>();
        if (observerBehaviour)
        {
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }

        if (descriptionCanvas != null)
            descriptionCanvas.alpha = 0;

        // Start the delay timer
        StartCoroutine(DelayFadeIn());
    }

    IEnumerator DelayFadeIn()
    {
        yield return new WaitForSeconds(initialDelay);
        delayFinished = true;
    }

    void OnDestroy()
    {
        if (observerBehaviour)
            observerBehaviour.OnTargetStatusChanged -= OnTargetStatusChanged;
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        isTracking = status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED;

        if (!isTracking && isVisible)
        {
            StopAllCoroutines();
            StartCoroutine(FadeOut());
        }
    }

    void Update()
    {
        if (!isTracking || isFading || descriptionCanvas == null || !delayFinished)
            return;

        float distance = Vector3.Distance(Camera.main.transform.position, transform.position);

        if (distance <= triggerDistance && !isVisible)
        {
            StopAllCoroutines();
            StartCoroutine(FadeIn());
        }
        else if (distance > triggerDistance && isVisible)
        {
            StopAllCoroutines();
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeIn()
    {
        isFading = true;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            descriptionCanvas.alpha = Mathf.Lerp(0, 1, elapsed / fadeDuration);
            yield return null;
        }

        descriptionCanvas.alpha = 1;
        isVisible = true;
        isFading = false;
    }

    IEnumerator FadeOut()
    {
        isFading = true;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            descriptionCanvas.alpha = Mathf.Lerp(1, 0, elapsed / fadeDuration);
            yield return null;
        }

        descriptionCanvas.alpha = 0;
        isVisible = false;
        isFading = false;
    }
}
