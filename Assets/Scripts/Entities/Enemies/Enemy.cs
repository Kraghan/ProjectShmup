using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    #region Attributes
    private float speed;
    private float acceleration;
    private float rotation;
    private float rotationAcceleration;
    private Rigidbody2D rgbd2D;
    #endregion

    #region MonoBehaviour main methods
    // Use this for initialization
    void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rgbd2D.velocity = gameObject.transform.forward * speed * Time.deltaTime;
        rgbd2D.rotation = rotation * Time.deltaTime;
        speed += acceleration * Time.deltaTime;
        rotation += rotationAcceleration * Time.deltaTime;
    }
    #endregion

    #region Methods
    
    #endregion
}
