using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorManager : MonoBehaviour {

    [SerializeField]
    IntVariable layerVar;

    [SerializeField]
    Color visibleValue;
    [SerializeField]
    Color invisibleValue;
    [SerializeField]
    float timeToFade;
    float timeElapsed;
    int oldLayerValue;
    bool completed;

    List<SpriteRenderer> backgrounds;
    List<SpriteRenderer> midgrounds;
    List<SpriteRenderer> foregrounds;

    // Use this for initialization
    void Start () {
        oldLayerValue = layerVar.value;
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        backgrounds = new List<SpriteRenderer>();
        midgrounds = new List<SpriteRenderer>();
        foregrounds = new List<SpriteRenderer>();
        for (int i = 0; i < renderers.Length; ++i)
        {
            if (renderers[i].gameObject.CompareTag("Background"))
            {
                backgrounds.Add(renderers[i]);
                renderers[i].color = invisibleValue;
            }
            if (renderers[i].gameObject.CompareTag("Midground"))
            {
                midgrounds.Add(renderers[i]);
                renderers[i].color = invisibleValue;
            }
            if (renderers[i].gameObject.CompareTag("Foreground"))
            {
                foregrounds.Add(renderers[i]);
                renderers[i].color = visibleValue;
            }
        }
        timeElapsed = 0;
        completed = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        if (completed && layerVar.value != oldLayerValue)
        {
            oldLayerValue = layerVar.value;
            completed = false;
            timeElapsed = 0;
        }

        timeElapsed += Time.deltaTime;
        float factor = timeElapsed / timeToFade;

        if (layerVar.value == 1)
        {
            bool layerCompleted = true;
            foreach(SpriteRenderer renderer in backgrounds)
            {
                if(renderer.color != invisibleValue)
                {
                    renderer.color = Color.Lerp(visibleValue,invisibleValue,factor);
                    layerCompleted = false;
                }
            }

            foreach (SpriteRenderer renderer in midgrounds)
            {
                if (renderer.color != invisibleValue)
                {
                    renderer.color = Color.Lerp(visibleValue, invisibleValue, factor);
                    layerCompleted = false;
                }
            }

            if (layerCompleted)
            {
                completed = true;
            }
        }
        else if (layerVar.value == 2)
        {
            bool layerCompleted = true;
            foreach (SpriteRenderer renderer in backgrounds)
            {
                if (renderer.color != invisibleValue)
                {
                    renderer.color = Color.Lerp(visibleValue, invisibleValue, factor);
                    layerCompleted = false;
                }
            }

            foreach (SpriteRenderer renderer in midgrounds)
            {
                if (renderer.color != visibleValue)
                {
                    renderer.color = Color.Lerp(invisibleValue, visibleValue, factor);
                    layerCompleted = false;
                }
            }

            if (layerCompleted)
            {
                completed = true;
            }
        }
        else if (layerVar.value == 3 || layerVar.value == 4)
        {
            bool layerCompleted = true;
            foreach (SpriteRenderer renderer in backgrounds)
            {
                if (renderer.color != visibleValue)
                {
                    renderer.color = Color.Lerp(invisibleValue, visibleValue, factor);
                    layerCompleted = false;
                }
            }

            foreach (SpriteRenderer renderer in midgrounds)
            {
                if (renderer.color != visibleValue)
                {
                    renderer.color = Color.Lerp(invisibleValue, visibleValue, factor);
                    layerCompleted = false;
                }
            }

            if(layerCompleted)
            {
                completed = true;
            }
        }
    }
}
