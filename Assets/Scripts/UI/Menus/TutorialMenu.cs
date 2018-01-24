using UnityEngine;

public class TutorialMenu : MonoBehaviour
{
    #region Attributes
    [SerializeField]
    private MenuScreen menuScreen;
    [SerializeField]
    private GameObject[] slides;

    private int selectedSlideID = 0;

    private TitleScreen titleScreen;
    private PauseScreen pauseScreen;
    #endregion

    #region MonoBehaviour main methods
    // Use this for initialization
    void Start()
    {
        switch (menuScreen)
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
    public void PreviousSlide()
    {
        selectedSlideID--;
        if(selectedSlideID < 0)
        {
            selectedSlideID = 0;
            InputDown_Cancel();
        }
        else
        {
            DisplayOnlySlide(selectedSlideID);
        }
    }

    public void NextSlide()
    {
        selectedSlideID++;
        if(selectedSlideID >= slides.Length)
        {
            selectedSlideID = slides.Length - 1;
            InputDown_Cancel();
        }
        else
        {
            DisplayOnlySlide(selectedSlideID);
        }
    }

    public void ResetSlides()
    {
        selectedSlideID = 0;
        DisplayOnlySlide(selectedSlideID);
    }

    private void DisplayOnlySlide(int slide)
    {
        for(int i = 0; i < slides.Length; i++)
        {
            if (i == slide)
                slides[i].SetActive(true);
            else
                slides[i].SetActive(false);
        }
    }

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
