using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Killable))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    #region Attributes
    [Tooltip("If true, the enemy dies when the player touch him")]
    [SerializeField]
    private bool dieOnPlayerHit = true;
    private Killable killable;
    #endregion

    #region MonoBehaviour main methods
    // Use this for initialization
    void Start()
    {
        killable = GetComponent<Killable>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region Methods
    public void HitPlayer()
    {
        if (dieOnPlayerHit)
            killable.ClearHealth();
    }
    #endregion
}
