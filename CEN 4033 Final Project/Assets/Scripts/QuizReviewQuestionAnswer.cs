using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizReviewQuestionAnswer : MonoBehaviour
{
    public TextMeshProUGUI answerText;

    public Image backgroundImage;

    public GameObject checkmark;

    public Color selectedCorrectColor;
    
    public Color selectedIncorrectColor;
    
    public Color missedCorrectColor;
    
    public Color defaultColor;

    public void Init(string text, bool selected, bool correct)
    {
        answerText.text = text;

        if (selected)
        {
            checkmark.SetActive(true);

            if(correct)
            {
                backgroundImage.color = selectedCorrectColor;
            }
            else
            {
                backgroundImage.color = selectedIncorrectColor;
            }
        }
        else
        {
            if(correct)
            {
                backgroundImage.color = missedCorrectColor;
            }
            else
            {
                backgroundImage.color = defaultColor;
            }
        }
    }

    void Update()
    {
        
    }
}
