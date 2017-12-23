using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PulseMode
{
    Scale,
    Circle
}

public class Pulser : MonoBehaviour
{
    #region Attributes
    [SerializeField]
    private PulseMode pulseMode;
    [SerializeField]
    private float timingPulse = 0.5f;

    private float currentTime;

    [Header("Pulse Mode - Scale")]
    [SerializeField]
    private float scaleIncrease = 1.3f;

    private Vector3 baseScale;

    [Header("Pulse Mode - Circle")]
    [SerializeField]
    private float circleMaxSize = 30;
    [SerializeField]
    private bool increase = true;

    [Header("Sprite modifiers")]
    [SerializeField]
    private bool modifyOpacity = false;
    [Range(0, 1)]
    [SerializeField]
    private float opacityStart;
    [Range(0, 1)]
    [SerializeField]
    private float opacityEnd;

    private SpriteRenderer spriteRenderer;
    #endregion

    #region MonoBehaviour main methods
    // Use this for initialization
    void Start () {
        baseScale = transform.localScale;
        if (!GetComponent<SpriteRenderer>().Equals(null))
            spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        // Pulser timing
        if (currentTime > 0)
            currentTime -= Time.deltaTime;
        Utility.Cap(ref currentTime, 0, timingPulse);
        if (currentTime == 0)
            Pulse();
        float percent = currentTime / timingPulse;

        // Pulser animation
        switch (pulseMode)
        {
            case PulseMode.Scale:
                transform.localScale = new Vector3(baseScale.x + (((baseScale.x * scaleIncrease) - baseScale.x) * percent), baseScale.y + (((baseScale.y * scaleIncrease) - baseScale.y) * percent), 1);
                break;
            case PulseMode.Circle:
                float circleSize = (increase ? ((1 - percent) * circleMaxSize) : (percent * circleMaxSize));
                transform.localScale = new Vector3(circleSize, circleSize, 1);
                break;  
            default:
                break;
        }

        // Pulser sprite modifiers
        if(spriteRenderer != null)
        {
            if(modifyOpacity)
            {
                float opacity = ((opacityStart <= opacityEnd) ? ((1-percent) * opacityEnd + opacityStart) : (percent * opacityStart + opacityEnd));
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, opacity);
            }
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
            case PulseMode.Circle:
                float circleSize = (increase ? 0 : circleMaxSize);
                transform.localScale = new Vector3(circleSize, circleSize, 1);
                break;
            default:
                break;
        }
        currentTime = timingPulse;
    }
    #endregion
}
