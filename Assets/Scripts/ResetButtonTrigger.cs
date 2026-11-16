using UnityEngine;

public class ResetButtonTrigger : MonoBehaviour
{
    public Camera arCamera;
    public QuizManager quizManager;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
            CheckRay(arCamera.ScreenPointToRay(Input.mousePosition));
#else
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            CheckRay(arCamera.ScreenPointToRay(Input.touches[0].position));
#endif
    }

    void CheckRay(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                quizManager.ResetQuiz();
                StartCoroutine(AnimatePress());
            }
        }
    }

    System.Collections.IEnumerator AnimatePress()
    {
        transform.localScale = originalScale * 0.85f;
        yield return new WaitForSeconds(0.1f);
        transform.localScale = originalScale;
    }
}
