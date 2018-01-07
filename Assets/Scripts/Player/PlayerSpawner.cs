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

    [SerializeField]
    private GameObject player;

    private GameObject currentPlayer;
    #endregion

    #region Monobehaviour
    // Use this for initialization
    void Start () {
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
                }
            }
        }
	}
    #endregion

    #region Methods
    public void createPlayer()
    {
        timeElapsed = 0;
        currentPlayer = Instantiate(player, transform.parent);
        currentPlayer.transform.position = transform.position;
        lifeVariable.value--;
    }
    #endregion
}
