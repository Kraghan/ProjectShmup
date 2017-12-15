using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PulseMode
{
    Scale
}

public class Pulser : MonoBehaviour
{
    #region Attributes
    [SerializeField]
    private FloatVariable pulseTimer;
    [SerializeField]
    private FloatVariable pulseTimerMax;
    [SerializeField]
    private PulseMode pulseMode;
    [SerializeField]
    private float scaleIncrease = 1.3f;

    private Vector3 baseScale;
    #endregion

    #region MonoBehaviour main methods
    // Use this for initialization
    void Start () {
        baseScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        if (pulseTimer.value <= 0)
        {
            transform.localScale = new Vector3(baseScale.x * scaleIncrease, baseScale.y * scaleIncrease, 1);
        }
        else
        {
            float percent = pulseTimer.value / pulseTimerMax.value;
            transform.localScale = new Vector3(baseScale.x + (((baseScale.x * scaleIncrease) - baseScale.x) * (percent/2)), baseScale.y + (((baseScale.y * scaleIncrease) - baseScale.y) * (percent/2)), 1);
        }
    }
    #endregion

    #region Methods
    #endregion
}
