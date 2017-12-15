using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
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
    public void Fire(string tagName, float _speed, float _acceleration, float _rotation, float _rotationAcceleration)
    {
        gameObject.tag = tagName;
        speed = _speed;
        acceleration = _acceleration;
        rotation = _rotation;
        rotationAcceleration = _rotationAcceleration;
    }
    #endregion
}
