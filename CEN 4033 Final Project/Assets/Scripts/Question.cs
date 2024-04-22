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

    // Adds new answer for the question
    public void AddAnswer(string answer, bool correct)
    {
        answers.Add(answer);

        // If this answer is correct, its index is added to the correct answers list
        if(correct) 
        {
            correctAnswers.Add(answers.Count - 1);
        }

        // If the number of correct answers is greater than 1, that means this question is multi-select
        if(correctAnswers.Count > 1)
        {
            multiSelect = true;
        }
    }
}
