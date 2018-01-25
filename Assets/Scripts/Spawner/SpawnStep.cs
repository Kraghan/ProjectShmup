using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class SpawnStep : MonoBehaviour {

    #region Attributes

    [SerializeField]
    GameObject enemy;

    [SerializeField]
    WaypointCircuit pattern;

    [SerializeField]
    int numberOfSpawn = 1;

    [SerializeField]
    float timeBetweenSpawn = 0.5f;
    float timeElapsed;
    GameObject pool;

    GameObject[] enemies;

    bool triggered;
    #endregion

    #region Monobehaviour

    // Use this for initialization
    void Start () {
        if (enemy == null)
            Debug.LogWarning("No enemy set in spawner step !");
        if (pattern == null)
            Debug.LogWarning("No patern set in spawner step !");

        timeElapsed = 0.0f;
        triggered = false;

        pattern = Instantiate(pattern,Camera.main.transform);
        pattern.transform.position = new Vector3(pattern.transform.position.x, pattern.transform.position.y, 0);

        enemies = new GameObject[numberOfSpawn];
        pool = GameObject.FindGameObjectWithTag("EnemyRepository");

        if(pool == null)
        {
            GameObject pools = Camera.main.gameObject;
            pool = new GameObject("Ennemy pool");
            pool.transform.parent = pools.transform;
        }

        for (int i = 0; i < numberOfSpawn; ++i)
        {
            GameObject newEnemy = Instantiate(enemy.gameObject, transform);
            enemies[i] = newEnemy;
            newEnemy.SetActive(false);
        }
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!triggered)
            return;

        timeElapsed += Time.deltaTime;
        if(numberOfSpawn != 0 && timeElapsed >= timeBetweenSpawn)
        {
            GameObject newEnemy = enemies[numberOfSpawn-1];
            newEnemy.transform.parent = pool.transform;
            newEnemy.transform.position = pattern.Waypoints[0].position;
            newEnemy.GetComponent<WaypointDeplacement>().SetPattern(pattern);

            newEnemy.SetActive(true);
            newEnemy.tag = "Enemy";

            timeElapsed -= timeBetweenSpawn;
            numberOfSpawn--;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("SpawnerTrigger"))
            triggered = true;
    }
    #endregion

    #region Methods

    #endregion
}
