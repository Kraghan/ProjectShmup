using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    #region Attributes
    private PauseScreen pauseScreen;
    #endregion

    #region MonoBehaviour main methods
    // Use this for initialization
    void Start()
    {
        pauseScreen = PauseScreen.Find();
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

    #region Inputs
    public void ButtonConfirm_Play()
    {
        SceneManager.LoadScene(1);
    }

    public void ButtonConfirm_Tutorial()
    {
        pauseScreen.GoTo_Tutorial();
    }

    public void ButtonConfirm_Options()
    {
        pauseScreen.GoTo_Options();
    }

    public void ButtonConfirm_TitleScreen()
    {
        SceneManager.LoadScene(0);
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
        pauseScreen.PlaySE_Confirm();
    }

    public void InputDown_Cancel()
    {
        pauseScreen.PlaySE_Cancel();
    }
    #endregion
    #endregion
}