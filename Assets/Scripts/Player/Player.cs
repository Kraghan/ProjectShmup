using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : Killable
{
    #region Attributes
    private GameObject bulletPool;
    private GameObject enemyPool;
    #endregion

    // Use this for initialization
    public override void Start () {
        base.Start();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        bulletPool = GameObject.FindGameObjectWithTag("BulletRepository");
        if (bulletPool)
        {
            for (int i = bulletPool.transform.childCount - 1; i >= 0; --i)
            {
                Destroy(bulletPool.transform.GetChild(i).gameObject);
            }
        }
        enemyPool = GameObject.FindGameObjectWithTag("EnemyRepository");
    }

    public override void OnDeath(bool onBeat)
    {
        /*if(enemyPool)
        {
            Pattern[] patterns = GetComponentsInChildren<Pattern>();
            for(int i = 0; i < patterns.Length; ++i)
            {
                patterns[i].ResetCooldown();
            }
        }*/
    }

    public override void OnHit(bool onBeat)
    {

    }
}
  