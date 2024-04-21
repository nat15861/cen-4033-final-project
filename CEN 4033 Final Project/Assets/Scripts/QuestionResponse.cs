using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionResponse
{
    public Question question;

    public List<int> userAnswers;

    public QuestionResponse(Question question, List<int> userAnswers)
    {
        this.question = question;

        this.userAnswers = userAnswers;
    } 
}
