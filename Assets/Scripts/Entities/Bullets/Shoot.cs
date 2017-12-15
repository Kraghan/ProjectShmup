using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shoot
{
    [Tooltip("Start speed")]
    public float speed;
    [Tooltip("Speed increase per second")]
    public float acceleration;
    [Tooltip("Starting direction")]
    public float direction;
    [Tooltip("Rotation modification per second")]
    public float rotation;
}
