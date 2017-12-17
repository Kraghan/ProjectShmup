using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shoot
{
    #region Attributes
    public bool largerDisplayInEditor = false;
    [Tooltip("Start speed")]
    public float speed;
    [Tooltip("Speed increase per second")]
    public float acceleration;
    [Tooltip("Starting direction")]
    public float direction;
    [Tooltip("Rotation modification per second")]
    public float rotation;
    #endregion

    #region Constructors
    public Shoot()
    {
        this.speed = 100;
        this.acceleration = 0;
        this.direction = 0;
        this.rotation = 0;
    }
    #endregion
}

