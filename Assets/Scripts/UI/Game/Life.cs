using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour {


    #region Attributes
    [SerializeField]
    private FloatVariable life;

    private Text text;
    private RawImage[] images;
    #endregion

    #region Monobehaviour
    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        images = GetComponentsInChildren<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < images.Length;++i)
        {
            if (i < life.value)
                images[i].gameObject.SetActive(true);
            else
                images[i].gameObject.SetActive(false);
        }
    }
    #endregion
}
