using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnLayer : MonoBehaviour {
    [SerializeField]
    private IntVariable layer;
    [SerializeField]
    private int layerLimit;
    private SpriteRenderer renderer;
	// Use this for initialization
	void Start () {
        renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (layer.value < layerLimit && !renderer.enabled)
            renderer.enabled = true;
        else if (layer.value >= layerLimit && renderer.enabled)
            renderer.enabled = false;
	}
}
