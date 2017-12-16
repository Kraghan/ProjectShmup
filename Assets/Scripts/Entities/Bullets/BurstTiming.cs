using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BurstTiming : ScriptableObject
{
    #region Attributes
    public float timing;
    public Burst burst;

    private bool done = false;
    #endregion

    #region Constructors
    public BurstTiming(float _timing, Burst _burst)
    {
        this.timing = _timing;
        this.burst = _burst;
    }
    #endregion

    #region Methods
    public void Done()
    {
        done = true;
    }

    public void Reset()
    {
        done = false;
    }
    #endregion

    #region Getters
    public bool IsDone()
    {
        return done;
    }
    #endregion
}
