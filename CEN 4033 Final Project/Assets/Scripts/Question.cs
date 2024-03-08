using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public string question;

    public List<string> answers;

    public List<int> correctAnswers;

    public string explanation;

    public bool multiSelect;

    public Question()
    {
        answers = new List<string>();

        correctAnswers = new List<int>();
    }

    public Question(string question, List<string> answers, List<int> correctAnswers, string explanation)
    {
        this.question = question;

        this.answers = answers;

        this.correctAnswers = correctAnswers;

        this.explanation = explanation;
    }

    public void AddAnswer(string answer, bool correct)
    {
        answers.Add(answer);

        if(correct) 
        {
            correctAnswers.Add(answers.Count - 1);
        }

        if(correctAnswers.Count > 1)
        {
            multiSelect = true;
        }
    }
}
