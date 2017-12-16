using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combo : MonoBehaviour {
    #region Attributes
    [SerializeField]
    private FloatVariable combo;

    private Text text;
    #endregion

    #region Monobehaviour
    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = combo.value.ToString("F0");
    }
    #endregion
}
