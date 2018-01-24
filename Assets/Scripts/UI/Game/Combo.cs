using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combo : MonoBehaviour {
    #region Attributes
    [SerializeField]
    private FloatVariable combo;
    [SerializeField]
    private Sprite[] sprites = new Sprite[4];
    private Image image;
    #endregion

    #region Monobehaviour
    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (combo.value > 1)
        {
            image.enabled = true;
            image.sprite = sprites[(int)combo.value - 2];
        }
        else
            image.enabled = false;
    }
    #endregion
}
