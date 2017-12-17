using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shoot
{
    #region Attributes
    [SerializeField]
    public bool largerDisplayInEditor = false;
    [Tooltip("Start speed")]
    [SerializeField]
    public float speed = 100;
    [Tooltip("Speed increase per second")]
    [SerializeField]
    public float acceleration = 0;
    [Tooltip("Starting direction")]
    [SerializeField]
    public float direction = 0;
    [Tooltip("Rotation modification per second")]
    [SerializeField]
    public float rotation = 0;
    #endregion
}

