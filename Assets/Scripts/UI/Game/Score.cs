using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    #region Attributes
    [SerializeField]
    private FloatVariable score;

    [SerializeField]
    private int numberOfZero = 9;

    private Text text;
    #endregion

    #region Monobehaviour
    // Use this for initialization
    void Start ()
    {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        string textToDisplay = "";

        for(int i = numberOfZero; i != 0; --i)
        {
            if (score.value < Mathf.Pow(10, i))
                textToDisplay += "0";
            else
                break;
        }

        textToDisplay += score.value.ToString("F0");

        text.text = textToDisplay;
	}
    #endregion
}
