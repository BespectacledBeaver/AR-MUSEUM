using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Mode : MonoBehaviour
{
    public Image cameraFilterOverlay;        // UI overlay image
    public float fadeDuration = 0.5f;
    public float visibleAlpha = 0.6f;
    public float transparentAlpha = 0f;

    public GameObject[] dayLights;           // Multiple day lights
    public GameObject[] nightLights;         // Multiple night lights

    public GameObject[] dayModeObjects;      // Objects only visible in day
    public GameObject[] nightModeObjects;    // Objects only visible in night

    private bool isFaded = false;            // true = night mode
    private Coroutine fadeCoroutine;
    private Camera arCamera;

    void Start()
    {
        arCamera = Camera.main;
        SetInitialAlpha(transparentAlpha);   // Start in day mode
        UpdateModeVisuals();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Ray ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform || hit.transform.IsChildOf(transform))
                {
                    ToggleFade();
                }
            }
        }
    }

    void ToggleFade()
    {
        float targetAlpha = isFaded ? transparentAlpha : visibleAlpha;

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeOverlay(targetAlpha));
        isFaded = !isFaded;

        UpdateModeVisuals();
    }

    IEnumerator FadeOverlay(float targetAlpha)
    {
        Color startColor = cameraFilterOverlay.color;
        float startAlpha = startColor.a;
        float time = 0f;

        while (time < fadeDuration)
        {
            float t = time / fadeDuration;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            cameraFilterOverlay.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
            time += Time.deltaTime;
            yield return null;
        }

        cameraFilterOverlay.color = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
    }

    void SetInitialAlpha(float alpha)
    {
        Color color = cameraFilterOverlay.color;
        color.a = alpha;
        cameraFilterOverlay.color = color;
    }

    void UpdateModeVisuals()
    {
        // Day lights
        if (dayLights != null)
        {
            foreach (GameObject light in dayLights)
                if (light != null) light.SetActive(!isFaded);
        }

        // Night lights
        if (nightLights != null)
        {
            foreach (GameObject light in nightLights)
                if (light != null) light.SetActive(isFaded);
        }

        // Day objects
        if (dayModeObjects != null)
        {
            foreach (GameObject obj in dayModeObjects)
                if (obj != null) obj.SetActive(!isFaded);
        }

        // Night objects
        if (nightModeObjects != null)
        {
            foreach (GameObject obj in nightModeObjects)
                if (obj != null) obj.SetActive(isFaded);
        }
    }
}
