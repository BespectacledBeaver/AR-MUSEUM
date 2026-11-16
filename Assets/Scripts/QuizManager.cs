using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string question;
        public string[] options;
        public string correctAnswer;
    }

    public List<Question> questions = new List<Question>();

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI[] optionTexts;
    public GameObject[] answerIndicators; // 6 spheres
    public GameObject resetButton;

    private int currentQuestionIndex = 0;
    private int totalQuestions;
    private bool quizCompleted = false;

    void Start()
    {
        totalQuestions = questions.Count;
        resetButton.SetActive(false);
        LoadQuestion();
        HideAllIndicators();
    }

    public void SelectAnswer(string selected)
    {
        if (quizCompleted) return;

        bool isCorrect = selected == questions[currentQuestionIndex].correctAnswer;
        SetIndicatorColor(currentQuestionIndex, isCorrect ? Color.green : Color.red);

        currentQuestionIndex++;

        if (currentQuestionIndex < totalQuestions)
        {
            LoadQuestion();
        }
        else
        {
            quizCompleted = true;
            resetButton.SetActive(true);
        }
    }

    void LoadQuestion()
    {
        Question q = questions[currentQuestionIndex];
        questionText.text = q.question;
        for (int i = 0; i < optionTexts.Length; i++)
        {
            optionTexts[i].text = q.options[i];
        }
    }

    void SetIndicatorColor(int index, Color color)
    {
        Renderer r = answerIndicators[index].GetComponent<Renderer>();
        if (r != null)
        {
            r.material.color = color;
        }
    }

    void HideAllIndicators()
    {
        foreach (GameObject indicator in answerIndicators)
        {
            Renderer r = indicator.GetComponent<Renderer>();
            if (r != null)
            {
                r.material.color = Color.gray;
            }
        }
    }

    public void ResetQuiz()
    {
        currentQuestionIndex = 0;
        quizCompleted = false;
        resetButton.SetActive(false);
        HideAllIndicators();
        LoadQuestion();
    }
}
