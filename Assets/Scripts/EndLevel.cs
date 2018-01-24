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
    float timeElapsed;


    private void Start()
    {
        triggered = false;
        timeElapsed = 0;
    }

    private void Update()
    {
        if (!triggered)
            return;
        
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= timeBeforeNextLevel)
        {
            SceneManager.LoadScene(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggered = true;
        textBoxForMessage.gameObject.SetActive(true);
    }
}
