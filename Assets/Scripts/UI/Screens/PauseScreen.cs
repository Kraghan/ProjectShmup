using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    #region Attributes
    [Header("Menu sections")]
    [SerializeField]
    private PauseMenu pauseMenu;
    [SerializeField]
    private TutorialMenu tutorialMenu;
    [SerializeField]
    private OptionsMenu optionsMenu;

    [Header("Audio")]
    [SerializeField]
    private AudioSource bgm_pauseScreen;
    [SerializeField]
    private AudioSource se_confirm;
    [SerializeField]
    private AudioSource se_cancel;
    [SerializeField]
    private AudioSource se_move;

    [Header("Graphics")]
    [SerializeField]
    private GameObject background;
    #endregion

    #region MonoBehaviour main methods
    // Use this for initialization
    void Start()
    {
        // Menus sections checks
        if (pauseMenu == null)
            Debug.LogWarning("PauseScreen - pauseMenu has not been assigned.");
        if (tutorialMenu == null)
            Debug.LogWarning("PauseScreen - tutorialMenu has not been assigned.");
        if (optionsMenu == null)
            Debug.LogWarning("PauseScreen - optionsMenu has not been assigned.");
        // AudioSources checks
        if (bgm_pauseScreen == null)
            Debug.LogWarning("PauseScreen - bgm_pauseScreen has not been assigned.");
        if (se_confirm == null)
            Debug.LogWarning("PauseScreen - se_confirm has not been assigned.");
        if (se_cancel == null)
            Debug.LogWarning("PauseScreen - se_cancel has not been assigned.");
        if (se_move == null)
            Debug.LogWarning("PauseScreen - se_move has not been assigned.");
        DeactivateMenuSections();
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region Methods
    public static PauseScreen Find()
    {
        GameObject pauseScreenGO = GameObject.FindGameObjectWithTag("PauseScreen");
        if (pauseScreenGO != null)
            return pauseScreenGO.SecureGetComponent<PauseScreen>();
        else
        {
            Debug.LogError("PauseScreen - Could not find the PauseScreen in the scene.");
            return null;
        }
    }

    #region Menu sections management

    public void GoTo_PauseMenu()
    {
        DeactivateMenuSections();
        AkSoundEngine.SetRTPCValue("LPF_Music", 70);
        pauseMenu.gameObject.SetActive(true);
    }

    public void GoTo_Tutorial()
    {
        DeactivateMenuSections();
        tutorialMenu.gameObject.SetActive(true);
    }

    public void GoTo_Options()
    {
        DeactivateMenuSections();
        optionsMenu.gameObject.SetActive(true);
    }

    public void DeactivateMenuSections()
    {
        pauseMenu.gameObject.SetActive(false);
        tutorialMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(false);
        AkSoundEngine.SetRTPCValue("LPF_Music", 0);
    }
    #endregion

    #region Audio management
    public void PlaySE_Confirm()
    {
        if (se_confirm == null)
            Debug.LogWarning("PauseScreen - se_confirm has not been assigned.");
        else
            se_confirm.Play();
    }

    public void PlaySE_Cancel()
    {
        if (se_cancel == null)
            Debug.LogWarning("PauseScreen - se_cancel has not been assigned.");
        else
            se_cancel.Play();
    }

    public void PlaySE_Move()
    {
        if (se_move == null)
            Debug.LogWarning("PauseScreen - se_move has not been assigned.");
        else
            se_move.Play();
    }
    #endregion
    #endregion

    #region Graphics
    public void BackgroundDisplay(bool _displayed)
    {
        background.SetActive(_displayed);
    }
    #endregion
}