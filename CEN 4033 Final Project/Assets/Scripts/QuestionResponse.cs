using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionResponse
{
    public Question question;

    public List<int> userAnswers;

    public float points;

    public QuestionResponse(Question question, List<int> userAnswers, float points)
    {
        this.question = question;

        this.userAnswers = userAnswers;

        this.points = points;
    } 
}
