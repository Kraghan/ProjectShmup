using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeaderboardMenu : MonoBehaviour
{
    #region Attributes
    public static event System.Action<GameJolt.API.Objects.Score[]> topTenLoader;

    [SerializeField]
    private InputField usernameInputField;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text topTenText;
    [SerializeField]
    private Text submitScoreComment;
    [SerializeField]
    private FloatVariable scoreVar;

    private bool scoreSubmitted = false;
    private TitleScreen titleScreen;
    #endregion

    #region MonoBehaviour methods
    // Use this for initialization
    void Start()
    {
        titleScreen = TitleScreen.Find();
        topTenLoader = LoadTopTen;
        if(scoreText != null && scoreVar != null)
        {
            scoreText.text = "Score: " + scoreVar.value.ToString() + " points";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            InputDown_Cancel();
        if (Input.GetButtonDown("Submit"))
            InputDown_Confirm();
    }
    #endregion

    #region Methods
    public void Refresh()
    {
        GetTop10();
    }

    public void GoToTitleScreen()
    {
        SceneManager.LoadScene(0);
    }

    public void GetTop10()
    {
        try
        {
            GameJolt.API.Scores.Get(topTenLoader, 0, 10, false);
        }
        catch (System.Exception e)
        {

        }
    }

    public void Click_Button_SubmitScore()
    {
        submitScoreComment.text = "";
        topTenText.text = "";
        float score = scoreVar.value;
        if (!scoreSubmitted)
        {
            if (usernameInputField.text != null && usernameInputField.text != "")
            {
                if (usernameInputField.text.Length <= 14)
                {
                    bool isOkay = SaveScoreOnline(usernameInputField.text, (int)score);
                    if (!isOkay)
                    {
                        submitScoreComment.text += "The score haven't been submitted correctly. Please verify your internet connection.";
                    }
                    else
                    {
                        submitScoreComment.text = "Submitted !";
                        scoreSubmitted = true;
                    }
                    bool isOkay2 = false;
                    try
                    {
                        GetTop10();
                        isOkay2 = true;
                    }
                    catch (System.Exception e)
                    {

                    }
                    if (!isOkay2)
                    {
                        topTenText.text = "Leaderboard can't be loaded. Please verify your internet connection.";
                    }
                    else
                    {

                    }
                }
                else
                    submitScoreComment.text = "Please enter less than 14 characters !";
            }
            else
                submitScoreComment.text = "Enter your name(s) first !";
        }
        else
            submitScoreComment.text = "Already submitted !";
    }

    // playerName must be less than 14 characters.
    public bool SaveScoreOnline(string playerName, int score)
    {
        bool isOkay = false;
        // Gamejolt API
        try
        {
            GameJolt.API.Scores.Add(score, (score.ToString() + " POINTS"), playerName, 0, "", (bool successScore) => {
                if (successScore)
                {

                }
                else
                {

                }
            });
            isOkay = true;
        }
        catch (System.Exception e)
        {
        }
        return isOkay;
    }

    public void LoadTopTen(GameJolt.API.Objects.Score[] scores)
    {
        try
        {
            topTenText.text = "";
            int maxI = 0;
            for (int i = 0; i < scores.Length && i < 10; i++)
            {
                topTenText.text += ScoreDisplayer(i + 1, scores[i]);
                maxI = i;
            }
            for (int j = (maxI + 1); j < 10; j++)
            {
                int rang = j + 1;
                topTenText.text += "#" + rang + " - X - X\n";
            }
        }
        catch (System.Exception exc)
        {
            topTenText.text = "Leaderboard can't be loaded. Please verify your internet connection.";
        }
    }

    public string ScoreDisplayer(int rank, GameJolt.API.Objects.Score score)
    {
        string displayer = "";
        displayer += "#" + rank.ToString() + " - ";
        displayer += score.Value.ToString() + " - ";
        displayer += score.PlayerName + "\n";
        return displayer;
    }

    #region Inputs
    public void InputDown_Confirm()
    {
        titleScreen.PlaySE_Confirm();
    }

    public void InputDown_Cancel()
    {
        titleScreen.PlaySE_Cancel();
        titleScreen.GoTo_MainMenu();
    }
    #endregion
    #endregion
}