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
        SaveData((questionResponses.ToArray(), score));
    }

    public async void SaveData((QuestionResponse[], float) quiz, string key = "")
    {
        //var data = new Dictionary<string, object> { { "keyName2", new Question("What's 9 + 10", new List<string>() { "3", "7", "9", "19"}, new List<int>() { 3 }, "9 + 10 is 19") } };
        //var asdf = new Dictionary<string, object> { { "keyName3", (new Question[] { new Question("asdf", new List<string>() { "3" }, new List<int>() { 0 }, "ssdf"), new Question("aasdfsdf", new List<string>() { "5" }, new List<int>() { 0 }, "ssd4545f") }, 1) } };
        
        if(key == "")
        {
            int index = await GetNumQuizzes() + 1;

            key = "Quiz" + index;
        }


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

    public async Task<QuizInfo[]> LoadAllQuizzes()
    {
        List<QuizInfo> quizzes = new List<QuizInfo>();

        try
        {
            var keys = await CloudSaveService.Instance.Data.Player.ListAllKeysAsync();

            for(int i = 0; i < keys.Count; i++)
            {
                var key = keys[i].Key;

                var quizData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { key });

                if (quizData.TryGetValue(key, out var quiz))
                {
                    (QuestionResponse[] questionResponses, float score) quizValue = quiz.Value.GetAs<(QuestionResponse[] questionResponses, float score)>();
                    
                    TimeZoneInfo local = TimeZoneInfo.Local;

                    quizzes.Add(new QuizInfo(quizValue.questionResponses, key.Substring(0, 4) + " " + key.Substring(4), quizValue.score, TimeZoneInfo.ConvertTimeFromUtc((quiz.Created ?? DateTime.MinValue), local)));
                }
            }

            return quizzes.ToArray();

        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        return null;
    }

    public async Task<int> GetNumQuizzes()
    {
        try
        {
            var keys = await CloudSaveService.Instance.Data.Player.ListAllKeysAsync();

            return keys.Count;

        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            
        }

        return -1;
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
