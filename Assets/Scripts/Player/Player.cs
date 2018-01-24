using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : Killable
{
    #region Attributes
    private GameObject bulletPool;
    private GameObject enemyPool;
    private Animator animator;
    private float initialLife;
    [SerializeField]
    private IntVariable hitVar;
    #endregion

    // Use this for initialization
    public override void Start () {
        base.Start();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        bulletPool = GameObject.FindGameObjectWithTag("BulletRepository");
        enemyPool = GameObject.FindGameObjectWithTag("EnemyRepository");
        animator = GetComponentInChildren<Animator>();
        initialLife = health;
        Revive();
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

    protected override void Die(bool onBeat)
    {
        hitVar.value = 0;
        animator.SetBool("IsAlive", false);
        AkSoundEngine.PostEvent("Player_Dead", gameObject);
        switch (deathAnimation)
        {
            case DeathAnimation.NoDeath:
                break;
            case DeathAnimation.NoAnimation:
                DisablePlayer();
                break;
            case DeathAnimation.InstantiatePrefab:
                if (deathAnimationPrefabToInstantiate != null)
                {
                    GameObject obj = Instantiate(deathAnimationPrefabToInstantiate, transform.position, Quaternion.identity, null);
                    obj.transform.parent = transform.parent;
                    AkSoundEngine.PostEvent("Acouphene", gameObject);
                }
                DisablePlayer();
                break;
            default:
                DisablePlayer();
                break;
        }

        OnDeath(onBeat);
    }

    public void DisablePlayer()
    {

        GetComponent<Collider2D>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
    }

    public void EnablePlayer()
    {
        GetComponent<Collider2D>().enabled = true;
        GetComponent<PlayerController>().enabled = true;
    }

    public void Revive()
    {
        if (bulletPool)
        {
            for (int i = bulletPool.transform.childCount - 1; i >= 0; --i)
            {
                Destroy(bulletPool.transform.GetChild(i).gameObject);
            }
        }

        health = initialLife;
        animator.SetBool("IsAlive", true);
        EnablePlayer();
        AkSoundEngine.PostEvent("Player_Respawn", gameObject);
    }
}
  