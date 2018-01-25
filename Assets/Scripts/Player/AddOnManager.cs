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
            {
                addonsLayer[i].SetActive(i <= (currentLayer.value - 1) ? true : false);
                /*
                if (GameObject.FindGameObjectWithTag("Player") != null)
                {
                    Vector3 addOnStartPos = GameObject.FindGameObjectWithTag("Player").transform.position;
                    addOnStartPos.x -= 50;
                    AddOn[] addOns = GameObject.FindObjectsOfType<AddOn>();
                    foreach (AddOn addOn in addOns)
                    {
                        if(addOn.gameObject.transform.position.x < addOnStartPos.x)
                            addOn.gameObject.transform.position = addOnStartPos;
                    }
                }*/
            }
        }
        layerPreviousFrame = currentLayer.value;
	}
}
