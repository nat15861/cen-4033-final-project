using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizInfo
{
    public QuestionResponse[] questionResponses;

    public string name;

    public float score;

    public DateTime timestamp;

    public QuizInfo(QuestionResponse[] questionResponses, string name, float score, DateTime timestamp) 
    {
        this.questionResponses = questionResponses;

        this.name = name;

        this.score = score;

        this.timestamp = timestamp;
    }

}
