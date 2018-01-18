using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOnManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] addonsLayer;
    [SerializeField]
    private IntVariable currentLayer;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        for(int i = 0; i < addonsLayer.Length; i++)
            addonsLayer[i].SetActive(i == (currentLayer.value - 1) ? true : false);
	}
}
