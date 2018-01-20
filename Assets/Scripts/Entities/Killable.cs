using System.Collections;
using System.Collections.Generic;
using ShmupPatternPackage;
using UnityEngine;

public enum DeathAnimation
{
    NoDeath,
    NoAnimation,
    InstantiatePrefab
}

[RequireComponent(typeof(Collider2D))]
public abstract class Killable : MonoBehaviour
{
    [Header("Killable properties")]
    [Tooltip("Time in seconds during which the Killable can't be damaged after taking damages")]
    [SerializeField]
    private float invulnerabilityTime;
    private float currentInvulnerabilityTime;
    [Tooltip("Comportement to adopt when health = 0")]
    [SerializeField]
    protected DeathAnimation deathAnimation = DeathAnimation.NoAnimation;
    [SerializeField]
    protected GameObject deathAnimationPrefabToInstantiate;
    [SerializeField]
    protected float health;

    [SerializeField]
    private float blinckingTime = 0.25f;
    private float timeElapsedSinceLastBlinkSwap;
    private bool isBlincking;
    protected SpriteRenderer spriteRenderer;

    public virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        isBlincking = false;
        timeElapsedSinceLastBlinkSwap = 0;

    }

    // Update is called once per frame
    void Update ()
    {
        if (currentInvulnerabilityTime > 0)
        {
            currentInvulnerabilityTime -= Time.deltaTime;
            timeElapsedSinceLastBlinkSwap += Time.deltaTime;

            if(timeElapsedSinceLastBlinkSwap >= blinckingTime)
            {
                timeElapsedSinceLastBlinkSwap = 0;
                if (isBlincking)
                {
                    spriteRenderer.enabled = false;
                    isBlincking = false;
                }
                else
                {
                    spriteRenderer.enabled = true;
                    isBlincking = true;
                }
            }
            
        }
        else
        {
            spriteRenderer.enabled = true;
            timeElapsedSinceLastBlinkSwap = 0;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!isInvincible())
        {
            bool playerDmg = gameObject.CompareTag("Player") && (collision.gameObject.CompareTag("EnemyBullet") || collision.gameObject.CompareTag("Enemy"));
            bool enemyDmg = gameObject.CompareTag("Enemy") && collision.gameObject.CompareTag("PlayerBullet");

            if(playerDmg || enemyDmg)
            {
                Bullet collidedBullet = collision.gameObject.GetComponent<Bullet>();

                if(collidedBullet != null)
                {
                    collidedBullet.Hit();
                    if (collidedBullet.GetDamages() == -1)
                        health = 0;
                    else
                        health -= collidedBullet.GetDamages();

                    OnHit(collidedBullet.isOnBeat);

                    if (health <= 0)
                        Die(collidedBullet.isOnBeat);
                    else // is alive
                    {
                        StartInvulnerabilityFrames();
                        /*if(m_sound.Length > 0)
                            AkSoundEngine.PostEvent(m_sound, gameObject);*/
                    }
                }
                // On est dans le cas où un ennemy collide avec le player
                else
                {
                    health--;
                    collision.gameObject.GetComponent<Enemy>().HitPlayer();

                    if (health <= 0)
                        Die(false);
                    else // is alive
                    {
                        StartInvulnerabilityFrames();
                        /*if(m_sound.Length > 0)
                            AkSoundEngine.PostEvent(m_sound, gameObject);*/
                    }

                }

                

            }
        }
    }

    public void StartInvulnerabilityFrames()
    {
        currentInvulnerabilityTime = invulnerabilityTime;
    }

    public bool isInvincible()
    {
        return (currentInvulnerabilityTime > 0);
    }

    public bool isAlive()
    {
        return health > 0;
    }

    protected virtual void Die(bool onBeat)
    {
        switch(deathAnimation)
        {
            case DeathAnimation.NoDeath:
                break;
            case DeathAnimation.NoAnimation:
                Destroy(gameObject);
                break;
            case DeathAnimation.InstantiatePrefab:
                if (deathAnimationPrefabToInstantiate != null)
                {
                    GameObject obj = Instantiate(deathAnimationPrefabToInstantiate, transform.position, Quaternion.identity, null);
                    obj.transform.parent = transform.parent;
                }
                    
                Destroy(gameObject);
                break;
            default:
                Destroy(gameObject);
                break;
        }

        OnDeath(onBeat);
    }

    public void ClearHealth()
    {
        if (currentInvulnerabilityTime > 0)
            return;
        health = 0;
    }

    public abstract void OnDeath(bool onBeat);
    public abstract void OnHit(bool onBeat);
}