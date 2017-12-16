using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PulseMode
{
    Scale,
    CircleLine
}

public class Pulser : MonoBehaviour
{
    #region Attributes
    [SerializeField]
    private PulseMode pulseMode;
    [SerializeField]
    private float scaleIncrease = 1.3f;
    [SerializeField]
    private float timingPulse = 0.5f;
    [SerializeField]
    private float circleMaxSize = 30;
    private float currentTime;

    private Vector3 baseScale;
    #endregion

    #region MonoBehaviour main methods
    // Use this for initialization
    void Start () {
        baseScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        if (currentTime > 0)
            currentTime -= Time.deltaTime;
        Utility.Cap(ref currentTime, 0, timingPulse);
        if (currentTime == 0)
            Pulse();
        float percent = currentTime / timingPulse;
        switch (pulseMode)
        {
            case PulseMode.Scale:
                transform.localScale = new Vector3(baseScale.x + (((baseScale.x * scaleIncrease) - baseScale.x) * percent), baseScale.y + (((baseScale.y * scaleIncrease) - baseScale.y) * percent), 1);
                break;
            case PulseMode.CircleLine:
                float circleSize = percent * circleMaxSize;
                transform.localScale = new Vector3(circleSize, circleSize, 1);
                break;
            default:
                break;
        }
        
    }
    #endregion

    #region Methods
    public void Pulse()
    {
        switch (pulseMode)
        {
            case PulseMode.Scale:
                transform.localScale = new Vector3(baseScale.x * scaleIncrease, baseScale.y * scaleIncrease, 1);
                break;
            case PulseMode.CircleLine:
                transform.localScale = new Vector3(circleMaxSize, circleMaxSize, 1);
                break;
            default:
                break;
        }
        currentTime = timingPulse;
    }
    #endregion
}
