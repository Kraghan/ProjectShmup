using System.Collections;
using System.Collections.Generic;
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
    [Tooltip("Time in seconds during which the Killable can't be damaged after taking damages")]
    [SerializeField]
    private float invulnerabilityTime;
    private float currentInvulnerabilityTime;
    [Tooltip("Comportement to adopt when health = 0")]
    [SerializeField]
    private DeathAnimation deathAnimation = DeathAnimation.NoAnimation;
    [SerializeField]
    private GameObject deathAnimationPrefabToInstantiate;
    [SerializeField]
    private float health;

    
	
	// Update is called once per frame
	void Update ()
    {
        if (currentInvulnerabilityTime > 0)
            currentInvulnerabilityTime -= Time.deltaTime;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!isInvincible())
        {
            bool playerDmg = (gameObject.tag == "Player" && collision.gameObject.tag == "EnemyBullet");
            bool enemyDmg = (gameObject.tag == "Enemy" && collision.gameObject.tag == "PlayerBullet");

            if(playerDmg || enemyDmg)
            {
                Bullet collidedBullet = collision.gameObject.GetComponent<Bullet>();

                collidedBullet.Hit();
                if (collidedBullet.GetDamages() == -1)
                    health = 0;
                else
                    health -= collidedBullet.GetDamages();

                OnHit(collidedBullet.isOnBeat);

                if (health <= 0)
                    Die();
                else // is alive
                {
                    currentInvulnerabilityTime = invulnerabilityTime;

                    if(m_sound.Length > 0)
                        AkSoundEngine.PostEvent(m_sound, gameObject);
                }
            }
        }
    }

    public bool isInvincible()
    {
        return currentInvulnerabilityTime > 0;
    }

    public bool isAlive()
    {
        return health > 0;
    }

    void Die(bool onBeat)
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