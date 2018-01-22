using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShmupPatternPackage;

public class Bomb : Bullet
{
    #region Attributes
    [SerializeField]
    float minRadius = 1.0f;
    [SerializeField]
    float maxRadius = 5.0f;
    [SerializeField]
    float speedSpread = 1.0f;

    bool parentHasParticleEmitter;
    #endregion

    #region Monobehaviour
    // Use this for initialization
    public override void Start () {
        base.Start();
        destroyOnHit = false;
        damages = -1;

        transform.localScale = new Vector3(minRadius, minRadius, transform.localScale.z);

        parentHasParticleEmitter = transform.parent.gameObject.GetComponent<ParticleSystem>() != null;

    }

    public override void Update()
    {
        base.Update();
        float scale = transform.localScale.x + speedSpread * Time.deltaTime;

        transform.localScale = new Vector3(scale,scale,transform.localScale.z);

        if (transform.localScale.x >= maxRadius)
        {
            if (!parentHasParticleEmitter)
                Destroy(gameObject);
            else
                Destroy(transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
        }
    }
    #endregion
}
