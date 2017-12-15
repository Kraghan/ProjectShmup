using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class WaypointCaracteristics : MonoBehaviour {
    #region Attributes
    [SerializeField]
    private float speedToReachThatPoint;
    [SerializeField]
    private float timeToStop;
    #endregion

    #region MonoBehaviour main methods
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    #endregion

    #region Methods
    public float GetSpeedToReach()
    {
        return speedToReachThatPoint;
    }

    public float GetTimeToStop()
    {
        return timeToStop;
    }
    #endregion
}
