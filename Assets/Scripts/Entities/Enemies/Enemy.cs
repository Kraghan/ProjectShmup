﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : Killable
{
    [Header("Score")]
    [SerializeField]
    private FloatVariable comboVariable;
    [SerializeField]
    private FloatVariable scoreVariable;
    [SerializeField]
    private int scoreOnHit = 0;
    [SerializeField]
    private int scoreOnKill = 100;
    [SerializeField]
    IntVariable m_combosCounter;

    [Tooltip("If true, the enemy dies when the player touch him")]
    [SerializeField]
    private bool dieOnPlayerHit = true;
    [SerializeField]
    private string m_sound;

    private Killable killable;
    
    // Use this for initialization
    public override void Start()
    {
        base.Start();
        killable = GetComponent<Killable>();
    }

    public void HitPlayer()
    {
        if (dieOnPlayerHit)
            killable.ClearHealth();
    }

    public override void OnDeath(bool onBeat)
    {
        if(onBeat)
        {
            if(m_sound.Length > 0)
                AkSoundEngine.PostEvent(m_sound, gameObject);
        }
        else
            AkSoundEngine.PostEvent("Enemy_destroy", gameObject);
        scoreVariable.value += scoreOnKill * Mathf.Pow(2, comboVariable.value - 1);
    }

    public override void OnHit(bool onBeat)
    {
        scoreVariable.value += scoreOnHit * Mathf.Pow(2, comboVariable.value - 1);
    }
}
