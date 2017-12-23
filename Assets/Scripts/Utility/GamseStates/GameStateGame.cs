using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateGame : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("escape"))
            Application.Quit();
	}
}
