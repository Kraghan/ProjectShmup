﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCalculator : MonoBehaviour {

    #region Attributes
    [SerializeField]
    FloatVariable comboVariable;
    [SerializeField]
    IntVariable hitCountVariable;
    [SerializeField]
    int[] hitNumberToUpCombo;
    #endregion

    #region Monobehaviour
    void Start()
    {

    }

    // Update is called once per frame
    void Update ()
    {

        if (hitCountVariable.value == 0)
        {
            comboVariable.value = 1;
            return;
        }

        if (hitNumberToUpCombo.Length == comboVariable.value)
            return;

        int hitTmp = hitCountVariable.value;

        for (int i = 0; i < comboVariable.value; ++i)
        {
            if (hitTmp < 0 || hitTmp < hitNumberToUpCombo[i])
                break;

            hitTmp -= hitNumberToUpCombo[i];
        }

        if (hitNumberToUpCombo[(int)comboVariable.value] <= hitTmp)
            comboVariable.value ++;
	}

    public int GetHitLevel(int combo)
    {
        int res = 0;
        for(int i = 0; i < combo; ++i)
        {
            res += hitNumberToUpCombo[i];
        }

        return res;
    }
    #endregion
}
