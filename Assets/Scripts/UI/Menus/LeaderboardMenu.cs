using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeaderboardMenu : MonoBehaviour
{
    #region Attributes
    public static event System.Action<GameJolt.API.Objects.Score[]> topLoader;

    [SerializeField]
    private InputField usernameInputField;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text col_1_score;
    [SerializeField]
    private Text col_1_name;
    [SerializeField]
    private Text col_2_score;
    [SerializeField]
    private Text col_2_name;
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
        topLoader = LoadTop;
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
        GetTop();
    }

    public void GoToTitleScreen()
    {
        SceneManager.LoadScene(0);
    }

    public void GetTop()
    {
        try
        {
            GameJolt.API.Scores.Get(topLoader, 0, 16, false);
        }
        catch (System.Exception e)
        {

        }
    }

    public void Click_Button_SubmitScore()
    {
        submitScoreComment.text = "";
        col_1_score.text = "";
        col_1_name.text = "";
        col_2_score.text = "";
        col_2_name.text = "";
        float score = scoreVar.value;
        if (!scoreSubmitted)
        {
            if (usernameInputField.text != null && usernameInputField.text != "")
            {
                if (usernameInputField.text.Length < 17)
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
                        GetTop();
                        isOkay2 = true;
                    }
                    catch (System.Exception e)
                    {

                    }
                    if (!isOkay2)
                    {
                        submitScoreComment.text = "Leaderboard can't be loaded. Please verify your internet connection.";
                    }
                    else
                    {

                    }
                }
                else
                    submitScoreComment.text = "Please enter less than 17 characters !";
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

    public void LoadTop(GameJolt.API.Objects.Score[] scores)
    {
        col_1_score.text = "";
        col_1_name.text = "";
        col_2_score.text = "";
        col_2_name.text = "";
        try
        {
            int maxI = 0;
            for (int i = 0; i < scores.Length && i < 8; i++)
            {
                if(i == 8 - 1)
                {
                    col_1_score.text += scores[i].Value.ToString();
                    col_1_name.text += scores[i].PlayerName.ToString();
                }
                else
                {
                    col_1_score.text += scores[i].Value.ToString() + "\n";
                    col_1_name.text += scores[i].PlayerName.ToString() + "\n";
                }
                maxI = i;
            }
            for (int j = (maxI + 1); j < 8; j++)
            {
                int rang = j + 1;
                if(j == 8 - 1)
                {
                    col_1_score.text += "   ";
                    col_1_name.text += "   ";
                }
                else
                {
                    col_1_score.text += "   " + "\n";
                    col_1_name.text += "   " + "\n";
                }
                
            }
            maxI = 8;
            for (int i = 8; i < scores.Length && i < 16; i++)
            {
                if (i == 16 - 1)
                {
                    col_2_score.text += scores[i].Value.ToString();
                    col_2_name.text += scores[i].PlayerName.ToString();
                }
                else
                {
                    col_2_score.text += scores[i].Value.ToString() + "\n";
                    col_2_name.text += scores[i].PlayerName.ToString() + "\n";
                }
                maxI = i;
            }
            for (int j = (maxI + 1); j < 16; j++)
            {
                int rang = j + 1;
                if (j == 16 - 1)
                {
                    col_2_score.text += "   ";
                    col_2_name.text += "   ";
                }
                else
                {
                    col_2_score.text += "   " + "\n";
                    col_2_name.text += "   " + "\n";
                }
            }
        }
        catch (System.Exception exc)
        {
            submitScoreComment.text = "Leaderboard can't be loaded. Please verify your internet connection.";
        }
    }

    public void Button_Replay()
    {
        AkSoundEngine.StopAll();
        TitleScreen.m_isMusicMenuPlaying = false;
        SceneManager.LoadScene(2);
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