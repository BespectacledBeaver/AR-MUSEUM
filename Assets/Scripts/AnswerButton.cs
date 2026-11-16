using UnityEngine;
using TMPro;

public class AnswerButton : MonoBehaviour
{
    public QuizManager quizManager;
    public TextMeshProUGUI answerLabel;

    public Camera arCamera;

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
                quizManager.SelectAnswer(answerLabel.text);
            }
        }
    }
}
