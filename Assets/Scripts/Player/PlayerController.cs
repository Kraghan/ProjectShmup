using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    #region Attributes
    [Header("Movements")]
    [SerializeField]
    private float horizontalSpeed;
    [SerializeField]
    private float verticalSpeed;

    [Header("Shoot")]
    [SerializeField]
    float m_errorWindow;
    [SerializeField]
    GameObject m_goodShot, m_badShot;

    private Player player;
    private Rigidbody2D rgbd2D;
    #endregion

    #region MonoBehaviour main methods
    // Use this for initialization
    void Start () {
        player = GetComponent<Player>();
        rgbd2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        ManageSpeed();

        if(Input.GetButtonDown("Fire1"))
            Fire();
	}
    #endregion

    #region Methods
    private void ManageSpeed()
    {
        rgbd2D.velocity = new Vector2(Time.deltaTime * horizontalSpeed * Input.GetAxis("Horizontal"), Time.deltaTime * verticalSpeed * Input.GetAxis("Vertical"));
    }

    void Fire()
    {
        if(BPM_Manager.IsOnBeat(m_errorWindow))
        {
            Instantiate(m_goodShot, transform.position, transform.rotation);
        }
        else
        {
            Instantiate(m_badShot, transform.position, transform.rotation);
        }
    }

    #region ColliderHit
    private void CheckColliderHit(Collider2D collider)
    {
        if (!CheckEnemyHit(collider))
            CheckSolidEnvironmentHit(collider);
    }

    private bool CheckEnemyHit(Collider2D potentialEnemy)
    {
        if (potentialEnemy.gameObject.tag == "Enemy")
        {
            // Do things
            return true;
        }
        else
            return false;
    }

    private bool CheckSolidEnvironmentHit(Collider2D potentialSolidEnvironment)
    {
        if (LayerMask.LayerToName(potentialSolidEnvironment.gameObject.layer) == "Solid")
        {
            // Do things
            return true;
        }
        else
            return false;
    }
    #endregion

    #region TriggerHit
    private void CheckTriggerHit(Collider2D trigger)
    {
        if (!CheckBulletHit(trigger))
            CheckPickUpHit(trigger);
    }

    private bool CheckBulletHit(Collider2D potentialBullet)
    {
        if (potentialBullet.gameObject.tag == "EnemyBullet")
        {
            // Do things
            return true;
        }
        else
            return false;
    }

    private bool CheckPickUpHit(Collider2D potentialPickUp)
    {
        if (potentialPickUp.gameObject.tag == "PickUp")
        {
            // Do things
            return true;
        }
        else
            return false;
    }
    #endregion
    #endregion

    #region MonoBehaviour methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckColliderHit(collision.otherCollider);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckColliderHit(collision.otherCollider);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckTriggerHit(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CheckTriggerHit(collision);
    }
    #endregion
}
