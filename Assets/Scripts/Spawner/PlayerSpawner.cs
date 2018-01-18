using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

		if(currentPlayer == null)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= timeBeforeRespawn)
            {
                if (lifeVariable.value != 0)
                    createPlayer();
                else
                {
                    // Todo : gameover

                    #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
                    #else
                        Application.Quit();
                    #endif
                }
            }
        }
	}
#endregion

#region Methods
    public void createPlayer()
    {
        timeElapsed = 0;
        currentPlayer = Instantiate(player.gameObject, transform);
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
