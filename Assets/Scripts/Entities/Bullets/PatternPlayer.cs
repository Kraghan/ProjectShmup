using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternPlayer : MonoBehaviour {

    [SerializeField]
    public Pattern pattern;

	// Use this for initialization
	void Start () {
        if (pattern == null)
            Debug.LogError("Pattern not set !");
        /*
        pattern = (Pattern)ScriptableObject.CreateInstance(typeof(Pattern));
        pattern.CopyProperties(pattern);*/
        pattern.PatternSetup();
	}
	
	// Update is called once per frame
	void Update () {
        pattern.PatternUpdate(gameObject);
	}
}
