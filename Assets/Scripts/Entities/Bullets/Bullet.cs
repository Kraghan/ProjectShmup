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
    private float direction;
    private float rotation;
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
        Vector3 directionVector = gameObject.transform.right;

        rgbd2D.velocity = directionVector * speed * Time.deltaTime;
        speed += (acceleration * Time.deltaTime);

        rgbd2D.rotation = direction;
        direction += (rotation * Time.deltaTime);
    }
    #endregion

    #region Methods
    public void Fire(float _speed, float _acceleration, float _direction, float _rotation)
    {
        speed = _speed;
        acceleration = _acceleration;
        direction = _direction;
        rotation = _rotation;
    }
    #endregion
}
