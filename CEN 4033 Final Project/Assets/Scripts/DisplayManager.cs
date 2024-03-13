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

    // Handles button clicks on a particular answer choice
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

    // Gives feedback to the user once the submit button is pressed
    public void SubmitAnswer()
    {
        if (state == State.Explain) return;


        // Show the question explanation in the title
        questionTitleText.text = currentQuestion.explanation;

        // Reveal all of the correct answers
        for (int i = 0; i < choices.Count; i++)
        {
            choices[i].Reveal();
        }

        // Enable the next button
        nextButton.Enable();

        // Update the state
        state = State.Explain;
    }

    // Switches to the next question if there is one
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

    // Displays the current question on the display by updating the title and the answer choices
    private void DisplayQuestion(int index)
    {
        currentQuestion = questionManager.questions[index];

        // Updating the title
        questionTitleText.text = currentQuestion.question;

        // Initializing the answer choices
        for(int i = 0; i < choices.Count; i++)
        {
            choices[i].Init(currentQuestion.answers[i], currentQuestion.correctAnswers.Contains(i), this);
        }
    }
}
