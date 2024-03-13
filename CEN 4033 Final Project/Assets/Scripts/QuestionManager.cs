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

    // Reads question information from Questions.txt and creates question objects with populated fields
    private void ReadQuestionData()
    {
        // Getting string data, dropping all the carriage returns
        string questionText = questionData.text.Replace("\r", "");

        // Splitting the file information into individual lines
        string[] questionLines = questionText.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        Question question = new Question();

        // Going through all the lines and using them to add attributes to new question objects
        // For each question, the question text should be first, follwed by the answer choices, and then the explanation last
        // To enforce this, any time a question attribute is added, we check that the other attributes that should follow it haven't been added yet
        for (int i = 0; i < questionLines.Length; i++)
        {
            string line = questionLines[i];

            if (line[0] == 'Q' && question.question == null && question.answers.Count == 0 && question.explanation == null)
            {
                question.question = line.Substring(3);
            }
            else if (line[0] == 'C' && question.explanation == null)
            {
                question.AddAnswer(line.Substring(3), true);
            }
            else if (line[0] == 'I' && question.explanation == null)
            {
                question.AddAnswer(line.Substring(3), false);
            }
            else if (line[0] == 'E' && question.explanation == null)
            {
                question.explanation = line.Substring(3);

                // Check to make sure that we have all the question attibutes, and if so, add the question to the list
                // Also create a new question object for the next question in the text file
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
