using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public TextMeshProUGUI loginText;

    public TextMeshProUGUI signUpText;

    public TMP_InputField usernameField;

    public TMP_InputField passwordField;

    public Button submitButton;

    public Color activeModeColor;

    public Color inactiveModeColor;
    
    public Mode mode = Mode.Login;

    public string username;

    public string password;

    public enum Mode
    {
        Login,
        SignUp
    }

    async void Awake()
    {
        try
        {
            await UnityServices.InitializeAsync();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        SetupEvents();
    }

    void SetupEvents()
    {
        AuthenticationService.Instance.SignedIn += () => {
            // Shows how to get a playerID
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

            // Shows how to get an access token
            Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");

        };

        AuthenticationService.Instance.SignInFailed += (err) => {
            Debug.LogError(err);
        };

        AuthenticationService.Instance.SignedOut += () => {
            Debug.Log("Player signed out.");
        };

        AuthenticationService.Instance.Expired += () =>
        {
            Debug.Log("Player session could not be refreshed and expired.");
        };
    }

    void Start()
    {
        UpdateModeButtons();

        submitButton.interactable = false;
    }

    void Update()
    {
        
    }

    public void ChangeMode(int intMode)
    {
        Mode newMode = (Mode)intMode;

        if (mode == newMode) return;


        mode = newMode;

        usernameField.text = "";

        passwordField.text = "";

        UpdateModeButtons();
    }

    private void UpdateModeButtons()
    {
        if(mode == Mode.Login)
        {
            loginText.color = activeModeColor;

            signUpText.color = inactiveModeColor;
        }
        else if(mode == Mode.SignUp)
        {
            loginText.color = inactiveModeColor;

            signUpText.color = activeModeColor;
        }
    }

    public void UpdateUsername(string newUsername)
    {
        username = newUsername;

        CheckValidFields();
    }

    public void UpdatePassword(string newPassword)
    {
        password = newPassword;

        CheckValidFields();
    }

    private void CheckValidFields()
    {
        submitButton.interactable = !username.Contains(" ") && !password.Contains(" ") && password.Length >= 10;
    }

    public async void SubmitCredentials()
    {
        if(mode == Mode.SignUp)
        {
            await SignUpWithUsernamePasswordAsync(username, password);
        }
        else if (mode == Mode.Login)
        {
            await SignInWithUsernamePasswordAsync(username, password);
        }
    }

    async Task SignUpWithUsernamePasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            Debug.Log("SignUp is successful.");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }

    async Task SignInWithUsernamePasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            Debug.Log("SignIn is successful.");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }
}
