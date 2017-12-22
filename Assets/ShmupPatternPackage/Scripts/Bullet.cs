using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShmupPatternPackage
{
    public enum DestroyAnimation
    {
        NoAnimation,
        InstantiatePrefab
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
        [SerializeField]
        private GameObject destroyAnimationPrefabToInstantiate;
        [SerializeField]
        private float destroyInXSeconds = 20;

        public bool isOnBeat = false;
        private float speed;
        private float acceleration;
        private float direction;
        private float rotation;

        private Rigidbody2D rgbd2D;
        #endregion

        #region MonoBehaviour main methods
        // Use this for initialization
        public virtual void Start()
        {
            rgbd2D = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        public virtual void Update()
        {
            Move();
            CheckOutOfBounds();
            
        }
        #endregion

        #region Methods
        public void Move()
        {
            // Initialization
            Vector3 directionVector = gameObject.transform.right;
            // Process
            rgbd2D.velocity = directionVector * speed * Time.deltaTime;
            speed += (acceleration * Time.deltaTime);
            rgbd2D.rotation = direction;
            direction += (rotation * Time.deltaTime);
        }

        public void CheckOutOfBounds()
        {
            // Initialization
            Vector2 positionOnScreen = Camera.main.WorldToScreenPoint(transform.position);
            // Process
            if (positionOnScreen.x > Screen.width || 0 > positionOnScreen.x)
                Destroy(gameObject);
            if (positionOnScreen.y > Screen.height || 0 > positionOnScreen.y)
                Destroy(gameObject);
        }

        public void ProgrammedDestruction()
        {
            destroyInXSeconds -= Time.deltaTime;
            if (destroyInXSeconds <= 0)
                DestroyBullet();
        }

        public void Fire(float _speed, float _acceleration, float _direction, float _rotation)
        {
            speed = _speed;
            acceleration = _acceleration;
            direction = _direction;
            if (direction < 180 && direction > 0)
                direction = (direction - (2 * direction)) % 360;
            else if (direction > 180 && direction < 360)
                direction = (direction + 2 * (360 - direction)) % 360;
            rotation = _rotation;
        }

        public void Hit()
        {
            if (destroyOnHit)
            {
                DestroyBullet();
            }
        }

        public void DestroyBullet()
        {
            switch (destroyAnimation)
            {
                case DestroyAnimation.NoAnimation:
                    Destroy(gameObject);
                    break;
                case DestroyAnimation.InstantiatePrefab:
                    if (destroyAnimationPrefabToInstantiate != null)
                    {
                        GameObject obj = Instantiate(destroyAnimationPrefabToInstantiate, transform.position, Quaternion.identity, null);
                        obj.transform.parent = transform.parent;
                    }
                    Destroy(gameObject);
                    break;
                default:
                    Destroy(gameObject);
                    break;
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
}