using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{
    private Button button;

    private Image image;

    private TextMeshProUGUI text;

    void Start()
    {
        button = GetComponent<Button>();

        image = GetComponent<Image>();

        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        
    }

    public void Enable()
    {
        button.interactable = true;

        image.enabled = true;

        text.enabled = true;
    }

    public void Disable()
    {
        button.interactable = false;

        image.enabled = false;

        text.enabled = false;
    }
}
