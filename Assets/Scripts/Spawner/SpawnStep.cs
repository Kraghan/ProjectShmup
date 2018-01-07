using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class SpawnStep : MonoBehaviour {

    #region Attributes

    [SerializeField]
    private Enemy enemy;

    [SerializeField]
    private WaypointCircuit pattern;

    [SerializeField]
    private Transform enemyPool;

    [SerializeField]
    private int numberOfSpawn = 1;

    [SerializeField]
    private float timeBetweenSpawn = 0.5f;
    private float timeElapsed;

    private bool triggered;
    #endregion

    #region Monobehaviour
    // Use this for initialization
    void Start () {
        if (enemy == null)
            Debug.LogWarning("No enemy set in spawner step !");
        if (pattern == null)
            Debug.LogWarning("No patern set in spawner step !");
        if(enemyPool == null)
        {
            GameObject g = GameObject.FindGameObjectWithTag("EnemyRepository");
            if (g == null)
                Debug.LogError("No object found with tag \"EnemyRepository\"");
            else
                enemyPool = g.transform;
        }

        timeElapsed = 0.0f;
        triggered = false;

        pattern = Instantiate(pattern,Camera.main.transform);
        pattern.transform.position = new Vector3(pattern.transform.position.x, pattern.transform.position.y, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!triggered)
            return;

        timeElapsed += Time.deltaTime;
        if(numberOfSpawn != 0 && timeElapsed >= timeBetweenSpawn)
        {
            GameObject newEnemy = Instantiate(enemy.gameObject, enemyPool);
            newEnemy.transform.position = pattern.Waypoints[0].position;
            newEnemy.GetComponent<WaypointDeplacement>().SetPattern(pattern);

            timeElapsed -= timeBetweenSpawn;
            numberOfSpawn--;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggered = true;
    }
    #endregion

    #region Methods

    #endregion
}
