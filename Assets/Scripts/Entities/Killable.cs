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
public class Killable : MonoBehaviour
{
    #region Attributes
    [Tooltip("Maximum health amount and starting health amount of the Killable")]
    [SerializeField]
    private float maxHealth;
    [Tooltip("Time in seconds during which the Killable can't be damaged after taking damages")]
    [SerializeField]
    private float invulnerabilityTime;
    private float currentInvulnerabilityTime;
    [Tooltip("Comportement to adopt when health = 0")]
    [SerializeField]
    private DeathAnimation deathAnimation = DeathAnimation.NoAnimation;
    [SerializeField]
    private GameObject deathAnimationPrefabToInstantiate;
    private float health;
    private bool alive = true;

    [SerializeField]
    private string m_sound;

    [Header("Score")]
    [SerializeField]
    private FloatVariable comboVariable;
    [SerializeField]
    private FloatVariable scoreVariable;
    [SerializeField]
    private int scoreOnHit = 0;
    [SerializeField]
    private int scoreOnKill = 100;

    #endregion

    #region MonoBehaviour main methods
    // Use this for initialization
    void Start () {
        health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        if (currentInvulnerabilityTime > 0)
            currentInvulnerabilityTime -= Time.deltaTime;
        Utility.Cap(ref currentInvulnerabilityTime, 0, invulnerabilityTime);
        if (health > 0)
            alive = true;
        if(alive && health <= 0)
            ProcDeathAnimation();
	}
    #endregion

    #region Methods
    public void ProcDeathAnimation()
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
                    Instantiate(deathAnimationPrefabToInstantiate, transform.position, Quaternion.identity, null);
                Destroy(gameObject);
                break;
            default:
                Destroy(gameObject);
                break;
        }
        alive = false;
    }

    private bool CheckBulletHit(Collider2D potentialBullet)
    {
        if (currentInvulnerabilityTime > 0)
            return false;
        bool playerDmg = (gameObject.tag == "Player" && potentialBullet.gameObject.tag == "EnemyBullet");
        bool enemyDmg = (gameObject.tag == "Enemy" && potentialBullet.gameObject.tag == "PlayerBullet");
        if(playerDmg || enemyDmg)
        {
            potentialBullet.gameObject.GetComponent<Bullet>().Hit();
            AddHealth(-potentialBullet.GetComponent<Bullet>().GetDamages());
            currentInvulnerabilityTime = invulnerabilityTime;

            if(m_sound.Length > 0)
                AkSoundEngine.PostEvent(m_sound, gameObject);

            if (enemyDmg)
            {
                scoreVariable.value += scoreOnHit;

                if (IsAlive())
                    scoreVariable.value += scoreOnKill;
            }

            return true;
        }
        else
            return false;
    }
    #endregion

    #region MonoBehaviour methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckBulletHit(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CheckBulletHit(collision);
    }
    #endregion

    #region Getters
    public bool IsAlive()
    {
        return alive;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetCurrentInvulnerabilityTime()
    {
        return currentInvulnerabilityTime;
    }
    #endregion

    #region Setters
    public void FillHealth()
    {
        health = maxHealth;
    }

    public void ClearHealth()
    {
        if (currentInvulnerabilityTime > 0)
            return;
        health = 0;
    }

    public void AddHealth(float amount)
    {
        health += amount;
        Utility.Cap(ref health, 0, maxHealth);
    }
    #endregion
}
