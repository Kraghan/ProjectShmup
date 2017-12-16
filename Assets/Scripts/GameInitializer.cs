using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour {
    #region Attributes
    [SerializeField]
    FloatVariable scoreVariable;
    [SerializeField]
    FloatVariable lifeVariable;
    [SerializeField]
    float startNumberOfLife;
    [SerializeField]
    FloatVariable comboVariable;

    #endregion

    #region Monobehaviour
    // Use this for initialization
    void Start () {
        scoreVariable.value = 0;
        comboVariable.value = 0;
        lifeVariable.value = startNumberOfLife;
	}
    #endregion
}
