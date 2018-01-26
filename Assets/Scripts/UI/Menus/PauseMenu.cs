using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    #region Attributes
    private PauseScreen pauseScreen;

    private bool canBeExitedWithCancel = false;
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
        if (Input.GetButtonUp("Cancel"))
            canBeExitedWithCancel = true;
        if (canBeExitedWithCancel && Input.GetButton("Cancel"))
            InputDown_Cancel();
        if (Input.GetButton("Submit"))
            InputDown_Confirm();
    }
    #endregion

    #region Methods

    #region Inputs
    public void ButtonConfirm_Play()
    {
        pauseScreen.DeactivateMenuSections();
        pauseScreen.BackgroundDisplay(false);
        Time.timeScale = 1;
        canBeExitedWithCancel = false;
        AkSoundEngine.SetRTPCValue("LPF_Music", 0);
    }

    public void ButtonConfirm_RestartLevel()
    {
        Time.timeScale = 1;
        AkSoundEngine.StopAll();
        TitleScreen.m_isMusicMenuPlaying = false;
        AkSoundEngine.SetRTPCValue("LPF_Music", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        Time.timeScale = 1;
        AkSoundEngine.SetRTPCValue("LPF_Music", 0);
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
        ButtonConfirm_Play();
    }
    #endregion
    #endregion
}