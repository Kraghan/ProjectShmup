using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shoot
{
    #region Attributes
    // Editor Attributes
    [SerializeField]
    public bool largerDisplayInEditor = false;

    // Attributes
    [SerializeField]
    public float speed = 100;
    [SerializeField]
    public float acceleration = 0;
    [SerializeField]
    public float direction = 0;
    [SerializeField]
    public float rotation = 0;
    [SerializeField]
    public Vector3 offset = new Vector3(0, 0, 0);
    #endregion
}

