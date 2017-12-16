using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DestroyAnimation
{
    NoAnimation
}

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    #region Attributes
    [SerializeField]
    protected float damages = 1;
    [SerializeField]
    protected bool destroyOnHit = true;
    [SerializeField]
    private DestroyAnimation destroyAnimation = DestroyAnimation.NoAnimation;

    public bool isOnBeat = false;

    private float destroyInXSeconds = 20;

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
        Vector2 positionOnScreen = Camera.main.WorldToScreenPoint(transform.position);

        Vector3 directionVector = gameObject.transform.right;

        rgbd2D.velocity = directionVector * speed * Time.deltaTime;
        speed += (acceleration * Time.deltaTime);

        rgbd2D.rotation = direction;
        direction += (rotation * Time.deltaTime);

        if (positionOnScreen.x > Screen.width || 0 > positionOnScreen.x)
            Destroy(gameObject);

        if (positionOnScreen.y > Screen.height || 0 > positionOnScreen.y)
            Destroy(gameObject);

        destroyInXSeconds -= Time.deltaTime;
        if (destroyInXSeconds <= 0)
            Destroy(gameObject);
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

    public void Hit()
    {
        if (destroyOnHit)
        {
            switch(destroyAnimation)
            {
                case DestroyAnimation.NoAnimation:
                    Destroy(gameObject);
                    break;
                default:
                    Destroy(gameObject);
                    break;
            }
        }
    }
    #endregion

    #region Getters
    public float GetDamages()
    {
        return damages;
    }
    #endregion
}
