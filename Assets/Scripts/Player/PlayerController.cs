using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Killable))]
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
    [SerializeField]
    private float focusSpeedMultiplicator;

    [Header("Shoot")]
    [SerializeField]
    float m_errorWindow;
    [SerializeField]
    GameObject m_goodShot, m_badShot;

    private Killable killable;
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
        killable = GetComponent<Killable>();
        player = GetComponent<Player>();
        bool focus = (Input.GetButton("Focus"));
        rgbd2D.velocity = new Vector2(Time.deltaTime * horizontalSpeed * (focus ? focusSpeedMultiplicator : 1) * Input.GetAxis("Horizontal"), Time.deltaTime * verticalSpeed * (focus ? focusSpeedMultiplicator : 1) * Input.GetAxis("Vertical"));
    }
    
    private void PickUp(GameObject pickup)
    {

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
        if (killable.GetCurrentInvulnerabilityTime() > 0)
            return;
        if (collider.gameObject.tag == "Enemy")
        {
            collider.gameObject.GetComponent<Enemy>().HitPlayer();
            killable.ClearHealth();
        }
    }
    #endregion

    #region TriggerHit
    private void CheckTriggerHit(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "PickUp")
            PickUp(trigger.gameObject);
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
