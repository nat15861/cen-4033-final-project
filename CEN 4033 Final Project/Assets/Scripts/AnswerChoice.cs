using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnswerChoice : MonoBehaviour, IPointerClickHandler
{
    private DisplayManager manager;

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
        choiceText = GetComponentInChildren<TextMeshProUGUI>();

        image = GetComponent<Image>();

        correctIconImage = transform.Find("Correct Icon").GetComponent<Image>();
    }

    public void Init(string text, bool correct, DisplayManager manager)
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

    public void Select()
    {
        selected = true;
        
        image.color = selectedColor;
    }

    public void Deselect()
    {
        selected = false;

        image.color = defaultColor;
    }

    public void Reveal()
    {
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
        

        if (correct)
        {
            correctIconImage.enabled = true;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (manager.state == DisplayManager.State.Explain) return;

        manager.ToggleChoice(this);
    }
}
