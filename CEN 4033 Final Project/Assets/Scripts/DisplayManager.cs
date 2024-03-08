using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour
{
    public QuestionManager questionManager;

    public TextMeshProUGUI questionTitleText;

    public Button submitButton;

    public NextButton nextButton;

    public List<AnswerChoice> choices = new List<AnswerChoice>();

    public Question currentQuestion;

    public int questionIndex = 0;


    public State state = State.Answer;

    public enum State
    {
        Answer,
        Explain
    }

    void Start()
    {
        questionManager.Init();

        DisplayQuestion(questionIndex);
    }

    void Update()
    {
        
    }

    public void ToggleChoice(AnswerChoice choice)
    {
        // If we are selecting this choice and the question isn't multi-choice, we need to deselect all the other choices 
        if(!choice.selected && !currentQuestion.multiSelect)
        {
            for (int i = 0; i < choices.Count; i++)
            {
                if (choices[i] != choice)
                {
                    choices[i].Deselect();
                }
            }
        }


        // Toggling the choice selction
        if(choice.selected)
        {
            choice.Deselect();
        }
        else
        {
            choice.Select();
        }

        
        // Making the button interactable if at least one of the choices is selected
        bool submitButtonActive = false;

        for (int i = 0; i < choices.Count; i++)
        {
            if (choices[i].selected)
            {
                submitButtonActive = true;

                break;
            }
        }

        submitButton.interactable = submitButtonActive;
    }

    public void SubmitAnswer()
    {
        if (state == State.Explain) return;


        questionTitleText.text = currentQuestion.explanation;

        for (int i = 0; i < choices.Count; i++)
        {
            choices[i].Reveal();
        }

        nextButton.Enable();

        state = State.Explain;
    }

    public void NextQuestion()
    {
        if (++questionIndex >= questionManager.questions.Count)
        {
            print("Out of questions!");
        }
        else
        {
            DisplayQuestion(questionIndex);

            nextButton.Disable();

            state = State.Answer;
        }
    }


    private void DisplayQuestion(int index)
    {
        currentQuestion = questionManager.questions[index];

        
        questionTitleText.text = currentQuestion.question;

        for(int i = 0; i < choices.Count; i++)
        {
            choices[i].Init(currentQuestion.answers[i], currentQuestion.correctAnswers.Contains(i), this);
        }
    }
}
