using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AccountWindow : MonoBehaviour
{
    public TextMeshProUGUI accountInfo;

    private void OnEnable()
    {
        accountInfo.text = "Username: " + PlayerManager.instance.username +
                           "\nPlayer ID: " + PlayerManager.instance.playerId +
                           "\nAccess Token: " + PlayerManager.instance.accessToken;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
