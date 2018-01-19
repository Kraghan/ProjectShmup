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
    [SerializeField]
    IntVariable hitVariable;
    [SerializeField]
    IntVariable bombVariable;
    [SerializeField]
    int startNumberOfBomb;

    #endregion

    #region Monobehaviour
    // Use this for initialization
    void Start () {
        scoreVariable.value = 0;
        comboVariable.value = 1;
        lifeVariable.value = startNumberOfLife;
        hitVariable.value = 0;
        bombVariable.value = startNumberOfBomb;
    }
    #endregion
}
