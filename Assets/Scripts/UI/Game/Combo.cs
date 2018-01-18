﻿using System.Collections;
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
        if (combo.value > 1)
            text.text = "x" + Mathf.Pow(2,combo.value - 1).ToString("F0");
        else
            text.text = "";
    }
    #endregion
}
