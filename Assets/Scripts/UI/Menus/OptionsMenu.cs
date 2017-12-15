using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    #region Attributes
    [SerializeField]
    private MenuScreen menuScreen;

    private TitleScreen titleScreen;
    private PauseScreen pauseScreen;
    #endregion

    #region MonoBehaviour methods
    // Use this for initialization
    void Start()
    {
        switch(menuScreen)
        {
            case MenuScreen.TitleScreen:
                titleScreen = TitleScreen.Find();
                break;
            case MenuScreen.PauseScreen:
                pauseScreen = PauseScreen.Find();
                break;
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

    #region Inputs
    public void InputDown_Confirm()
    {
        switch (menuScreen)
        {
            case MenuScreen.TitleScreen:
                titleScreen.PlaySE_Confirm();
                break;
            case MenuScreen.PauseScreen:
                pauseScreen.PlaySE_Confirm();
                break;
        }
    }

    public void InputDown_Cancel()
    {
        switch (menuScreen)
        {
            case MenuScreen.TitleScreen:
                titleScreen.PlaySE_Cancel();
                titleScreen.GoTo_MainMenu();
                break;
            case MenuScreen.PauseScreen:
                pauseScreen.PlaySE_Cancel();
                pauseScreen.GoTo_PauseMenu();
                break;
        }
        
    }
    #endregion
    #endregion
}
