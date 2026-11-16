using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClickInfo : MonoBehaviour
{
    public CanvasGroup descriptionGroup;
    public float fadeDuration = 0.5f;
    public Image infoButtonImage;
    public Color normalColor = Color.white;
    public Color dimmedColor = new Color(0.3f, 0.3f, 0.3f, 1f);

    private bool isVisible = false;
    private bool isTouching = false;
    private bool inputReady = false;
    private Coroutine fadeCoroutine;

    IEnumerator Start()
    {
        if (descriptionGroup != null)
        {
            descriptionGroup.alpha = 0f;
            descriptionGroup.interactable = false;
            descriptionGroup.blocksRaycasts = false;
        }

        if (infoButtonImage != null)
        {
            infoButtonImage.color = normalColor;
        }

        yield return new WaitForSeconds(0.5f); // debounce to avoid initial accidental touch
        inputReady = true;
    }

    void Update()
    {
        if (!inputReady || fadeCoroutine != null) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (Physics.Raycast(ray, out hit) && (hit.transform == transform || hit.transform.IsChildOf(transform)))
                    {
                        isTouching = true;
                        if (infoButtonImage != null)
                            infoButtonImage.color = dimmedColor;
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (isTouching)
                    {
                        isTouching = false;

                        if (infoButtonImage != null)
                            infoButtonImage.color = normalColor;

                        if (Physics.Raycast(ray, out hit) && (hit.transform == transform || hit.transform.IsChildOf(transform)))
                        {
                            ToggleDescription();
                        }
                    }
                    break;
            }
        }
    }

    void ToggleDescription()
    {
        if (descriptionGroup == null) return;

        float from = descriptionGroup.alpha;
        float to = isVisible ? 0f : 1f;
        fadeCoroutine = StartCoroutine(Fade(descriptionGroup, from, to));
        isVisible = !isVisible;
    }

    IEnumerator Fade(CanvasGroup cg, float from, float to)
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            cg.alpha = Mathf.Lerp(from, to, t);
            yield return null;
        }

        cg.alpha = to;
        if (to < 0.1f)
        {
            var audioPlayer = GetComponentInChildren<AudioPlayer>();
            if (audioPlayer != null)
                audioPlayer.StopAndReset();
        }

        cg.interactable = (to > 0.9f);
        cg.blocksRaycasts = (to > 0.9f);

        fadeCoroutine = null; // unlock input
    }
}
