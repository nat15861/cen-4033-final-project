using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public LoginManager loginManager;

    public GameObject menuOptions;

    public GameObject signedInDisplay;

    public GameObject accountWindow;
    
    public State state = State.Login;

    public enum State
    {
        Menu,
        Login,
        Account,
        Credits
    }

    void Start()
    {
        if(PlayerManager.instance.playerId != "")
        {
            loginManager.CloseWindow();

            menuOptions.SetActive(true);

            signedInDisplay.SetActive(true);

            state = State.Menu;
        }
    }
     
    void Update()
    {
        
    }

    public void FinishLogin()
    {
        loginManager.CloseWindow();

        menuOptions.SetActive(true);

        signedInDisplay.SetActive(true);

        state = State.Menu;
    }

    public void LogOut()
    {
        PlayerManager.instance.ClearPlayerData();

        accountWindow.SetActive(false);



        loginManager.OpenWindow();

        state = State.Login;
    }

    public void OpenAccount()
    {
        menuOptions.SetActive(false);

        signedInDisplay.SetActive(false);

        accountWindow.SetActive(true);

        state = State.Account;
    }

    public void CloseAccount()
    {
        accountWindow.SetActive(false);

        menuOptions.SetActive(true);

        signedInDisplay.SetActive(true);

        state = State.Menu;
    }

    public void OpenCredits()
    {
        menuOptions.SetActive(false);

        signedInDisplay.SetActive(false);

        //accountOptions.SetActive(true);

        state = State.Credits;
    }

    public void CloseCredits()
    {
        //accountOptions.SetActive(false);

        menuOptions.SetActive(true);

        signedInDisplay.SetActive(true);

        state = State.Menu;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
