using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOnManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] addonsLayer;
    [SerializeField]
    private IntVariable currentLayer;
    private int layerPreviousFrame;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < addonsLayer.Length; i++)
            addonsLayer[i].SetActive(i <= (currentLayer.value - 1) ? true : false);
    }
	
	// Update is called once per frame
	void Update () {
        if(layerPreviousFrame != currentLayer.value)
        {
            for (int i = 0; i < addonsLayer.Length; i++)
                addonsLayer[i].SetActive(i <= (currentLayer.value - 1) ? true : false);
        }
        layerPreviousFrame = currentLayer.value;
	}
}
