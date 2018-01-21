using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    #region Attributes
    private TitleScreen titleScreen;
    #endregion

    #region MonoBehaviour methods
    // Use this for initialization
    void Start () {
        titleScreen = TitleScreen.Find();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Cancel"))
            InputDown_Cancel();
        if (Input.GetButtonDown("Submit"))
            InputDown_Confirm();
	}
    #endregion

    #region Methods

    #region Inputs
    public void ButtonConfirm_Play()
    {
        SceneManager.LoadScene(2);
    }

    public void ButtonConfirm_Tutorial()
    {
        titleScreen.GoTo_Tutorial();
    }

    public void ButtonConfirm_Leaderboard()
    {
        titleScreen.GoTo_Leaderboard();
    }

    public void ButtonConfirm_Options()
    {
        titleScreen.GoTo_Options();
    }

    public void ButtonConfirm_Credits()
    {
        titleScreen.GoTo_Credits();
    }

    public void ButtonConfirm_QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void InputDown_Confirm()
    {
        titleScreen.PlaySE_Confirm();
    }

    public void InputDown_Cancel()
    {
        titleScreen.PlaySE_Cancel();
    }
    #endregion
    #endregion
}
