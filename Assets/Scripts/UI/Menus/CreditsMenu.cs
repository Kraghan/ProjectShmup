using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    #region Attributes
    private TitleScreen titleScreen;
    #endregion

    #region MonoBehaviour main methods
    // Use this for initialization
    void Start()
    {
        titleScreen = TitleScreen.Find();
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