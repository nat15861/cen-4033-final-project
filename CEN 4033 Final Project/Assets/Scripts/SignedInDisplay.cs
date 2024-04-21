using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SignedInDisplay : MonoBehaviour
{
    private TextMeshProUGUI text;

    void Start()
    {
        
    }

    private void OnEnable()
    {
        if(text == null)
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        text.text = "Signed in as " + PlayerManager.instance.username;
    }

    void Update()
    {
        
    }
}
