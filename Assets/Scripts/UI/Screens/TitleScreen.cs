using UnityEngine;

public class TitleScreen : MonoBehaviour
{

    #region Attributes
    [Header("Menu sections")]
    [SerializeField]
    private MainMenu mainMenu;
    [SerializeField]
    private TutorialMenu tutorialMenu;
    [SerializeField]
    private LeaderboardMenu leaderboardMenu;
    [SerializeField]
    private OptionsMenu optionsMenu;
    [SerializeField]
    private CreditsMenu creditsMenu;

    [Header("Audio")]
    [SerializeField]
    private AudioSource bgm_titleScreen;
    [SerializeField]
    private AudioSource se_confirm;
    [SerializeField]
    private AudioSource se_cancel;
    [SerializeField]
    private AudioSource se_move;
    #endregion

    #region MonoBehaviour main methods
    // Use this for initialization
    void Start () {
        // Menus sections checks
        if (mainMenu == null)
            Debug.LogWarning("TitleScreen - mainMenu has not been assigned.");
        if (tutorialMenu == null)
            Debug.LogWarning("TitleScreen - tutorialMenu has not been assigned.");
        if (leaderboardMenu == null)
            Debug.LogWarning("TitleScreen - leaderboardMenu has not been assigned.");
        if (optionsMenu == null)
            Debug.LogWarning("TitleScreen - optionsMenu has not been assigned.");
        if (creditsMenu == null)
            Debug.LogWarning("TitleScreen - creditsMenu has not been assigned.");
        // AudioSources checks
        if (bgm_titleScreen == null)
            Debug.LogWarning("TitleScreen - bgm_titleScreen has not been assigned.");
        if (se_confirm == null)
            Debug.LogWarning("TitleScreen - se_confirm has not been assigned.");
        if (se_cancel == null)
            Debug.LogWarning("TitleScreen - se_cancel has not been assigned.");
        if (se_move == null)
            Debug.LogWarning("TitleScreen - se_move has not been assigned.");
        GoTo_MainMenu();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    #endregion

    #region Methods
    public static TitleScreen Find()
    {
        GameObject titleScreenGO = GameObject.FindGameObjectWithTag("TitleScreen");
        if (titleScreenGO != null)
            return titleScreenGO.SecureGetComponent<TitleScreen>();
        else
        {
            Debug.LogError("TitleScreen - Could not find the TitleScreen in the scene.");
            return null;
        }
    }

    #region Menu sections management
    public void GoTo_MainMenu()
    {
        DeactivateMenuSections();
        mainMenu.gameObject.SetActive(true);
    }

    public void GoTo_Tutorial()
    {
        DeactivateMenuSections();
        tutorialMenu.ResetSlides();
        tutorialMenu.gameObject.SetActive(true);
    }

    public void GoTo_Leaderboard()
    {
        DeactivateMenuSections();
        leaderboardMenu.gameObject.SetActive(true);
    }

    public void GoTo_Options()
    {
        DeactivateMenuSections();
        optionsMenu.gameObject.SetActive(true);
    }

    public void GoTo_Credits()
    {
        DeactivateMenuSections();
        creditsMenu.gameObject.SetActive(true);
    }

    public void DeactivateMenuSections()
    {
        mainMenu.gameObject.SetActive(false);
        tutorialMenu.gameObject.SetActive(false);
        leaderboardMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(false);
    }
    #endregion

    #region Audio management
    public void PlaySE_Confirm()
    {
        if (se_confirm == null)
            Debug.LogWarning("TitleScreen - se_confirm has not been assigned.");
        else
            se_confirm.Play();
    }

    public void PlaySE_Cancel()
    {
        if (se_cancel == null)
            Debug.LogWarning("TitleScreen - se_cancel has not been assigned.");
        else
            se_cancel.Play();
    }

    public void PlaySE_Move()
    {
        if (se_move == null)
            Debug.LogWarning("TitleScreen - se_move has not been assigned.");
        else
            se_move.Play();
    }
    #endregion
    #endregion
}