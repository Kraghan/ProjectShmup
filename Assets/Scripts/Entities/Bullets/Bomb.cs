using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Bullet
{
    #region Attributes
    [SerializeField]
    float minRadius = 1.0f;
    [SerializeField]
    float maxRadius = 5.0f;
    [SerializeField]
    float speedSpread = 1.0f;
    #endregion

    #region Monobehaviour
    // Use this for initialization
    void Start () {
        destroyOnHit = false;
        damages = -1;

        transform.localScale = new Vector3(minRadius, minRadius, transform.localScale.z);

    }

    private void Update()
    {
        float scale = transform.localScale.x + speedSpread * Time.deltaTime;

        transform.localScale = new Vector3(scale,scale,transform.localScale.z);

        if (transform.localScale.x >= maxRadius)
            Destroy(gameObject);
    }
    #endregion
}
