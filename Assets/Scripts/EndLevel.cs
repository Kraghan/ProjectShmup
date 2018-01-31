    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour {

    bool triggered;
    [SerializeField]
    Text textBoxForMessage;
    [SerializeField]
    float timeBeforeNextLevel = 3;
    [Header("Score")]
    [SerializeField]
    FloatVariable scoreVar;
    [SerializeField]
    IntVariable bombVar;
    [SerializeField]
    FloatVariable lifeVar;
    [SerializeField]
    float bombBonus;
    [SerializeField]
    float lifeBonus;


    private void Start()
    {
        triggered = false;
    }

    private void Update()
    {
        if (!triggered)
            return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!triggered)
        {
            textBoxForMessage.gameObject.SetActive(true);
            scoreVar.value += bombVar.value * bombBonus + lifeBonus * lifeVar.value;
            AkSoundEngine.StopAll();
            TitleScreen.m_isMusicMenuPlaying = false;
            AkSoundEngine.PostEvent("GameOver", gameObject);
            Invoke("GotoBoard", timeBeforeNextLevel);
        }
        triggered = true;
    }

    void GotoBoard()
    {
        SceneManager.LoadScene(1);
    }
}