using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public QuestionManager questionManager;

    public GameObject questionDisplay;

    public GameObject quizResults;

    public TextMeshProUGUI questionTitleText;

    public Button submitButton;

    public NextButton nextButton;

    public List<AnswerChoice> choices = new List<AnswerChoice>();

    public TextMeshProUGUI quizScoreText;

    public Question currentQuestion;

    public int questionIndex = 0;

    public float score;


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


        List<int> userAnswers = new List<int>();

        // Reveal all of the correct answers
        for (int i = 0; i < choices.Count; i++)
        {
            choices[i].Reveal();

            if (choices[i].selected)
            {
                userAnswers.Add(i);
            }
        }

        float points = CalculatePoints(userAnswers);

        PlayerManager.instance.AddQuestionResponse(new QuestionResponse(currentQuestion, userAnswers, points));

        score += points;


        // Enable the next button
        nextButton.Enable();

        // Update the state
        state = State.Explain;
    }

    private float CalculatePoints(List<int> userAnswers)
    {
        if (!currentQuestion.multiSelect)
        {
            return currentQuestion.correctAnswers[0] == userAnswers[0] ? 1 : 0;
        }

        float points = 0;

        // The amount of points each answer will give. Each correct answer will add this amount, and each incorrect answer will subtract this amount
        float answerPoints = 1f / currentQuestion.correctAnswers.Count;

        for(int i = 0; i < userAnswers.Count; i++)
        {
            if (currentQuestion.correctAnswers.Contains(userAnswers[i]))
            {
                points += answerPoints;
            }
            else
            {
                points -= answerPoints;
            }
        }

        return Mathf.Max(points, 0);
    }

    // Switches to the next question if there is one
    public void NextQuestion()
    {
        if (++questionIndex >= questionManager.questions.Count)
        {
            questionDisplay.SetActive(false);

            quizResults.SetActive(true);

            score = (float)System.Math.Round(score, 1);

            quizScoreText.text = "Score: " + score + "/10";

            PlayerManager.instance.SubmitQuiz(score);
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

    public void LoadMainMenu()
    {
        PlayerManager.instance.questionResponses = new List<QuestionResponse>();

        SceneManager.LoadScene(0);
    }
}
