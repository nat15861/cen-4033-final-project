using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizReviewWindow : MonoBehaviour
{
    public QuizReviewQuestion questionPref;

    public QuizInfo quizInfo;

    public TextMeshProUGUI quizNameText;

    public TextMeshProUGUI quizScoreText;
    
    public TextMeshProUGUI quizTimestampText;

    public Transform questionParent;

    public ScrollRect scrollRect;

    public List<QuizReviewQuestion> questions = new List<QuizReviewQuestion>();

    public void Init(QuizInfo quizInfo)
    {
        this.quizInfo = quizInfo;

        quizNameText.text = quizInfo.name;

        quizScoreText.text = "Score: " + quizInfo.score + " / 10";
        
        quizTimestampText.text = "Attempted: " + quizInfo.timestamp;

        scrollRect.verticalNormalizedPosition = 1;

        for(int i = 0; i < quizInfo.questionResponses.Length; i++)
        {
            QuizReviewQuestion question = Instantiate(questionPref, questionParent);

            question.Init(quizInfo.questionResponses[i], i);

            questions.Add(question);
        }
    }



    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnDisable()
    {
        for (int i = 0; i < questions.Count; i++)
        {
            Destroy(questions[i].gameObject);
        }

        questions.Clear();
    }
}
