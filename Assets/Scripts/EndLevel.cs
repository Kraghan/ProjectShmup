    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            Application.Quit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggered = true;
        textBoxForMessage.text = "End of the level reached ! Thanks for playing";
    }
}
