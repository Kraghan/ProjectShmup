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
    private Vector2 borderOffset;
    [Range(0f, 1f)]
    [SerializeField]
    private float rawInputPercent;
    [SerializeField]
    private float focusSpeedMultiplicator;

    [Header("Shoot")]
    [SerializeField]
    private float m_shootCooldown;
    private float m_shootTimeElapsed;
    [SerializeField]
    private float m_errorWindowPerfect;
    [SerializeField]
    private float m_errorWindowGreat;
    [SerializeField]
    private float m_errorWindowGood;
    [SerializeField]
    private GameObject m_goodShot;
    [SerializeField]
    private GameObject m_greatShot;
    [SerializeField]
    private GameObject m_perfectShot;
    [SerializeField]
    private GameObject m_badShot;
    [SerializeField]
    private GameObject m_bomb;
    [SerializeField]
    private IntVariable m_bombVariable;
    [SerializeField]
    Transform m_shotPool;
    [SerializeField]
    IntVariable m_hitCounter;
    [SerializeField]
    FloatVariable m_combosCounter;
    ComboCalculator m_calculator;


    private Killable killable;
    private Player player;
    private Rigidbody2D rgbd2D;
    private Animator m_animator;
    private float m_numberOfFailConsecutive;
    #endregion

    #region MonoBehaviour main methods
    
    void Start ()
    {
        player = GetComponent<Player>();
        rgbd2D = GetComponent<Rigidbody2D>();
        killable = GetComponent<Killable>();
        m_animator = GetComponentInChildren<Animator>();
        m_numberOfFailConsecutive = 0;

        m_combosCounter.value = 0;
        if (m_shotPool == null)
            m_shotPool = GameObject.FindGameObjectWithTag("BulletRepository").transform;
        if (m_goodShot == null)
            Debug.LogError("PlayerController - m_goodShot is not assigned ! You can't shoot on the beat ! Please assign a prefab that contains a PatterPlayer to be able to shoot.");
        if (m_badShot == null)
            Debug.LogError("PlayerController - m_badShot is not assigned ! You can't shoot off the beat ! Please assign a prefab that contains a PatterPlayer to be able to shoot.");

        GameObject tmp = GameObject.FindGameObjectWithTag("GameManager");
        m_calculator = tmp.GetComponent<ComboCalculator>();
    }
	
	void Update () {
        m_shootTimeElapsed += Time.deltaTime;
        ManageSpeed();

        if(Input.GetButtonDown("Cancel"))
        {
            PauseScreen pauseScreen = GameObject.FindGameObjectWithTag("PauseScreen").GetComponent<PauseScreen>();
            pauseScreen.BackgroundDisplay(true);
            pauseScreen.DeactivateMenuSections();
            pauseScreen.GoTo_PauseMenu();
            Time.timeScale = 0;
        }

        if(m_shootTimeElapsed >= m_shootCooldown && Input.GetButtonDown("Fire1"))
        {
            Fire();
            m_shootTimeElapsed -= m_shootCooldown;
        }
        if(Input.GetButtonDown("Bomb"))
        {
            Bomb();
        }
	}
    #endregion

    #region Methods
    private void ManageSpeed()
    {
        bool focus = (Input.GetButton("Focus"));

        Vector2 positionOnScreen = Camera.main.WorldToScreenPoint(transform.position);

        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        float inputVertical = Input.GetAxisRaw("Vertical");
        
        inputVertical *= rawInputPercent;
        inputHorizontal *= rawInputPercent;
        inputVertical += ((1 - rawInputPercent) * Input.GetAxis("Vertical"));
        inputHorizontal += ((1 - rawInputPercent) * Input.GetAxis("Horizontal"));

        rgbd2D.velocity = new Vector2(Time.deltaTime * horizontalSpeed * (focus ? focusSpeedMultiplicator : 1) * inputHorizontal, rgbd2D.velocity.y);
        rgbd2D.velocity = new Vector2(rgbd2D.velocity.x, Time.deltaTime * verticalSpeed * (focus ? focusSpeedMultiplicator : 1) * inputVertical);

        if (positionOnScreen.x + borderOffset.x > Screen.width && rgbd2D.velocity.x > 0 || 0 > positionOnScreen.x - borderOffset.x && rgbd2D.velocity.x < 0)
            rgbd2D.velocity = new Vector2(0, rgbd2D.velocity.y);

        if (positionOnScreen.y + borderOffset.y > Screen.height && rgbd2D.velocity.y > 0 || 0 > positionOnScreen.y - borderOffset.y && rgbd2D.velocity.y < 0)
            rgbd2D.velocity = new Vector2(rgbd2D.velocity.x, 0);
    }


    
    private void PickUp(GameObject pickup)
    {

    }

    void Fire()
    {
        GameObject newProj;

        if(BPM_Manager.IsOnBeat(m_errorWindowPerfect))
        {
            AkSoundEngine.PostEvent("Bullet", gameObject);
            newProj = Instantiate(m_perfectShot, transform.position, transform.rotation);

            m_hitCounter.value ++;
            m_numberOfFailConsecutive = 0;

            //m_animator.SetTrigger("GoodShot");
        }
        else if (BPM_Manager.IsOnBeat(m_errorWindowGreat))
        {
            AkSoundEngine.PostEvent("Bullet", gameObject);
            newProj = Instantiate(m_greatShot, transform.position, transform.rotation);

            m_hitCounter.value++;
            m_numberOfFailConsecutive = 0;

            //m_animator.SetTrigger("GoodShot");
        }
        else if (BPM_Manager.IsOnBeat(m_errorWindowGood))
        {
            AkSoundEngine.PostEvent("Bullet", gameObject);
            newProj = Instantiate(m_goodShot, transform.position, transform.rotation);

            m_hitCounter.value++;
            m_numberOfFailConsecutive = 0;

            //m_animator.SetTrigger("GoodShot");
        }
        else
        {
            AkSoundEngine.PostEvent("Bullet_fail", gameObject);
            newProj = Instantiate(m_badShot, transform.position, transform.rotation);

            m_numberOfFailConsecutive++;

            if(m_numberOfFailConsecutive == 2)
            {
                m_combosCounter.value--;
                m_numberOfFailConsecutive = 0;
                if (m_combosCounter.value < 0)
                    m_combosCounter.value = 0;
                m_hitCounter.value = m_calculator.GetHitLevel((int)m_combosCounter.value);
            }
        }

        newProj.transform.SetParent(m_shotPool);
    }

    void Bomb()
    {
        if (m_bombVariable.value <= 0)
            return;

        GameObject newProj;
        
        //AkSoundEngine.PostEvent("Bullet_fail", gameObject);
        m_combosCounter.value = 0;
        newProj = Instantiate(m_bomb, transform.position, transform.rotation);

        newProj.transform.SetParent(m_shotPool);
        m_bombVariable.value--;
    }

    #region ColliderHit
    private void CheckColliderHit(Collider2D collider)
    {

    }
    #endregion

    #region TriggerHit
    private void CheckTriggerHit(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "PickUp")
            PickUp(trigger.gameObject);
        else if (trigger.gameObject.tag == "Enemy")
        {
            if (!killable.isInvincible())
            {
                trigger.gameObject.GetComponent<Enemy>().HitPlayer();
                killable.ClearHealth();
            }
        }
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

    #region Getters
    public float GetHorizontalSpeed()
    {
        return horizontalSpeed;
    }

    public float GetVerticalSpeed()
    {
        return verticalSpeed;
    }

    public float GetFocusSpeedMultiplicator()
    {
        return focusSpeedMultiplicator;
    }
    #endregion
}
