using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public string playerId;

    public string accessToken;

    public string username;

    public List<QuestionResponse> questionResponses = new List<QuestionResponse>();

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);

            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if(Input.GetKeyDown(KeyCode.P)) 
        {
            //username = (await AuthenticationService.Instance.GetPlayerInfoAsync()).Username
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            print("saving");

            //SaveData();
        }

        if (Input.GetKeyDown("l"))
        {
            print("yo");

            LoadData();
        }
    }

    public void AddQuestionResponse(QuestionResponse questionResponse)
    {
        questionResponses.Add(questionResponse);
    }

    public void SubmitQuiz(float score)
    {
        SaveData("Quiz1", (questionResponses.ToArray(), score));
    }

    public async void SaveData(string key, (QuestionResponse[], float) quiz)
    {
        //var data = new Dictionary<string, object> { { "keyName2", new Question("What's 9 + 10", new List<string>() { "3", "7", "9", "19"}, new List<int>() { 3 }, "9 + 10 is 19") } };
        //var asdf = new Dictionary<string, object> { { "keyName3", (new Question[] { new Question("asdf", new List<string>() { "3" }, new List<int>() { 0 }, "ssdf"), new Question("aasdfsdf", new List<string>() { "5" }, new List<int>() { 0 }, "ssd4545f") }, 1) } };

        var data = new Dictionary<string, object> { { key, quiz } };

        try
        {
            await CloudSaveService.Instance.Data.Player.SaveAsync(data);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

    }

    public async void LoadData()
    {
        try
        {
            var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { "Quiz1" });

            if (playerData.TryGetValue("Quiz1", out var keyName))
            {
                Debug.Log($"keyName: {keyName.Value.GetAs<(QuestionResponse[] questionResponses, float score)>()}");
                Debug.Log($"keyName: {keyName.Value.GetAs<(QuestionResponse[] questionResponses, float score)>().score}");
                Debug.Log($"keyName: {keyName.Value.GetAs<(QuestionResponse[] questionResponses, float score)>().questionResponses}");
                Debug.Log($"keyName: {keyName.Value.GetAs<(QuestionResponse[] questionResponses, float score)>().questionResponses[0]}");
                Debug.Log($"keyName: {keyName.Value.GetAs<(QuestionResponse[] questionResponses, float score)>().questionResponses[0].points}");
                Debug.Log($"keyName: {keyName.Value.GetAs<(QuestionResponse[] questionResponses, float score)>().questionResponses[0].userAnswers}");
                Debug.Log($"keyName: {keyName.Value.GetAs<(QuestionResponse[] questionResponses, float score)>().questionResponses[0].question}");
                Debug.Log($"keyName: {keyName.Value.GetAs<(QuestionResponse[] questionResponses, float score)>().questionResponses[0].question.question}");
            }
            else
            {
                print("No data found for keyName");
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    async Task Test()
    {
        username = (await AuthenticationService.Instance.GetPlayerInfoAsync()).Username;

        playerId = AuthenticationService.Instance.PlayerId;

        accessToken = AuthenticationService.Instance.AccessToken;
    }

    public async Task GetPlayerInfo(string playerId, string accessToken)
    {
        this.playerId = playerId;

        this.accessToken = accessToken;

        username = (await AuthenticationService.Instance.GetPlayerInfoAsync()).Username;
    }

    public void ClearPlayerData()
    {
        playerId = "";

        accessToken = "";

        username = "";
    }
}
