using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Bar : MonoBehaviour
{
    #region Attributes
    [SerializeField]
    private FloatVariable maximum;
    [SerializeField]
    private FloatVariable value;
    [SerializeField]
    private bool invertFill = false;

    private Image bar;
    #endregion

    #region MonoBehaviour main methods
    // Use this for initialization
    void Start()
    {
        bar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update () {
        bar.fillAmount = ((!invertFill) ? Utility.Percent(value.value, maximum.value) : Utility.InvertPercent(value.value, maximum.value));
	}
    #endregion
}
