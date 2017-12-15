using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BurstTiming
{
    #region Attributes
    public float timing;
    public Burst burst;

    private bool done = false;
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
