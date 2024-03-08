using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public TextAsset questionData;

    public List<Question> questions;

    public void Init()
    {
        ReadQuestionData();
    }

    void Update()
    {
        
    }

    private void ReadQuestionData()
    {
        string questionText = questionData.text.Replace("\r", "");

        string[] questionLines = questionText.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        Question question = new Question();

        for (int i = 0; i < questionLines.Length; i++)
        {
            string line = questionLines[i];

            if (line[0] == 'Q' && question != null && question.question == null)
            {
                question.question = line.Substring(3);
            }
            else if (line[0] == 'C' && question != null)
            {
                question.AddAnswer(line.Substring(3), true);
            }
            else if (line[0] == 'I' && question != null)
            {
                question.AddAnswer(line.Substring(3), false);
            }
            else if (line[0] == 'E' && question != null && question.explanation == null)
            {
                question.explanation = line.Substring(3);

                if (question.question != null && question.answers.Count > 0 && question.correctAnswers.Count > 0)
                {
                    questions.Add(question);

                    question = new Question();
                }
                else
                {
                    print("ERROR: Attributes missing for question, make sure Question.txt formatting is correct");

                    return;
                }
            }
            else
            {
                print("ERROR: Problem reading Questions.txt, make sure formatting is correct");

                return;
            }
        }
    }
}
