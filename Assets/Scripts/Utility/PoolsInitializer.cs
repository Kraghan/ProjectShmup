using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShmupPatternPackage;

public class PoolsInitializer : MonoBehaviour {

    [SerializeField]
    private GameObject[] objects;

	// Use this for initialization
	void Start ()
    {
		for(int i = 0; i < objects.Length; ++i)
        {
            GameObject obj = new GameObject("Pool - " + objects[i].name.Replace("(Clone)",""));
            Pool pool = obj.AddComponent<Pool>();
            pool.Initialize(objects[i]);
        }
	}
}
