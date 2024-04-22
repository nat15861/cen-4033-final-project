using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizReviewQuestion : MonoBehaviour
{
    public TextMeshProUGUI questionNameText;

    public TextMeshProUGUI questionPointsText;

    public TextMeshProUGUI questionText;

    public Question question;

    public List<int> userAnswers;

    public float points;

    public int index;

    public QuizReviewQuestionAnswer[] answerChoices = new QuizReviewQuestionAnswer[4];

    public void Init(QuestionResponse questionResponse, int index)
    {
        question = questionResponse.question;   

        userAnswers = questionResponse.userAnswers;
        
        points = questionResponse.points;


        questionNameText.text = "Question " + (index + 1);

        questionPointsText.text = points + " / 1 pts";

        questionText.text = question.question;


        for(int i = 0; i < answerChoices.Length; i++)
        {
            string answerText = question.answers[i];

            bool correct = question.correctAnswers.Contains(i);

            bool selected = userAnswers.Contains(i);

            answerChoices[i].Init(answerText, selected, correct);
        }
    }

    void Update()
    {
        
    }
}
