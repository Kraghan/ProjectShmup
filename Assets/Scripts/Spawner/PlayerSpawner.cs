﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    #region Attributes
    [SerializeField]
    private FloatVariable lifeVariable;

    [SerializeField]
    private float timeBeforeRespawn = 1;
    private float timeElapsed;
    
    private bool firstRespawn;

    [SerializeField]
    private Player player;

    private GameObject currentPlayer;
    #endregion

    #region Monobehaviour
    // Use this for initialization
    void Start () {
        firstRespawn = true;
        createPlayer();
	}
	
	// Update is called once per frame
	void Update () {

		if(currentPlayer == null || !currentPlayer.GetComponent<Player>().isAlive())
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= timeBeforeRespawn)
            {
                if (lifeVariable.value != 0)
                    createPlayer();
                else
                {
                    SceneManager.LoadScene(1);
                }
            }
        }
	}
#endregion

    #region Methods
    public void createPlayer()
    {
        timeElapsed = 0;
        if (currentPlayer == null)
            currentPlayer = Instantiate(player.gameObject, transform);
        else
            currentPlayer.GetComponent<Player>().Revive();
        currentPlayer.transform.position = transform.position;
        if (!firstRespawn)
        {
            lifeVariable.value--;
            
            currentPlayer.GetComponent<Player>().StartInvulnerabilityFrames();
        }
        else
        {
            firstRespawn = false;
        }
            
    }
#endregion
}
