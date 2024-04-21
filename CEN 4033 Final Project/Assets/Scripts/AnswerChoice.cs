using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnswerChoice : MonoBehaviour, IPointerClickHandler
{
    private QuizManager manager;

    private TextMeshProUGUI choiceText;

    private Image image;

    public Image correctIconImage;

    private bool correct;


    public Color defaultColor;

    public Color selectedColor;

    public Color correctColor;

    public Color incorrectColor;

    public bool selected = false;


    void Awake()
    {
        // Grabbing our components

        choiceText = GetComponentInChildren<TextMeshProUGUI>();

        image = GetComponent<Image>();

        correctIconImage = transform.Find("Correct Icon").GetComponent<Image>();
    }

    // Initializing the answer choice with the given fields
    public void Init(string text, bool correct, QuizManager manager)
    {
        choiceText.text = text;

        this.correct = correct;

        this.manager = manager;


        selected = false;

        image.color = defaultColor;

        correctIconImage.enabled = false;
    }

    void Update()
    {
        
    }

    // Selects this answer choice
    public void Select()
    {
        selected = true;
        
        image.color = selectedColor;
    }

    // Deselects this answer choice
    public void Deselect()
    {
        selected = false;

        image.color = defaultColor;
    }

    // Reveals whether or not this answer choice was correct
    public void Reveal()
    {
        // Selected answers will display red or green depending on if they were correct
        if (selected)
        {
            if (correct)
            {
                image.color = correctColor;
            }
            else
            {
                image.color = incorrectColor;
            }
        }
        
        // Correct answers will display a checkmark next to them
        if (correct)
        {
            correctIconImage.enabled = true;
        }
    }

    // Handles button clicks on this answer choice
    public void OnPointerClick(PointerEventData eventData)
    {
        if (manager.state == QuizManager.State.Explain) return;

        manager.ToggleChoice(this);
    }
}
