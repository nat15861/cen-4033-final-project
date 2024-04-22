using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccountWindow : MonoBehaviour
{
    public GameObject quizPreviewPref;
    
    public TextMeshProUGUI accountInfo;

    public Transform quizPreviewParent;

    public GameObject mainAccountMenu;

    public QuizReviewWindow quizReview;

    public QuizInfo[] quizzes;

    public List<GameObject> quizPreviews = new List<GameObject>();


    private async void OnEnable()
    {
        accountInfo.text = "Username: " + PlayerManager.instance.username +
                           "\nPlayer ID: " + PlayerManager.instance.playerId +
                           "\nAccess Token: " + PlayerManager.instance.accessToken;

        quizzes = await PlayerManager.instance.LoadAllQuizzes();

        for(int i = 0; i < quizzes.Length; i++)
        {
            int index = i;

            QuizInfo quiz = quizzes[i];

            GameObject quizPreview = Instantiate(quizPreviewPref, quizPreviewParent);

            quizPreview.transform.Find("Quiz Name").GetComponent<TextMeshProUGUI>().text = quiz.name;

            quizPreview.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = quiz.score + "/10";

            quizPreview.transform.Find("View Quiz Button").GetComponent<Button>().onClick.AddListener(() => ViewQuiz(index));

            quizPreviews.Add(quizPreview);
        }


    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void ViewQuiz(int index)
    {
        mainAccountMenu.SetActive(false);

        quizReview.gameObject.SetActive(true);

        quizReview.Init(quizzes[index]);


        print(index);
    }

    public void CloseQuiz()
    {
        quizReview.gameObject.SetActive(false);

        mainAccountMenu.SetActive(true);
    }

    private void OnDisable()
    {
        for(int i = 0; i < quizPreviews.Count; i++)
        {
            Destroy(quizPreviews[i]);
        }

        quizPreviews.Clear();
    }
}
