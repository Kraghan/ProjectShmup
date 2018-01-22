using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBeforeDestroy : MonoBehaviour {

    [SerializeField]
    float secondsBeforeDestroy = 0.5f;
    float timeElapsed;

    void Start()
    {
        timeElapsed = 0;
    }

    // Update is called once per frame
    void Update () {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= secondsBeforeDestroy)
            Destroy(gameObject);
	}
}
