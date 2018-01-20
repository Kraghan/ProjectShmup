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
                foregrounds.Add(renderers[i]);
        }
        timeElapsed = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Color valueToReach = Color.Lerp(invisibleValue, visibleValue, timeToFade);
        if (layerVar.value != oldLayerValue)
        {
            oldLayerValue = layerVar.value;   
        }

        if(layerVar.value == 1)
        {
            foreach(SpriteRenderer renderer in backgrounds)
            {
                if(renderer.color != invisibleValue)
                {
                    renderer.color -= valueToReach;
                    if(renderer.color.a < invisibleValue.a)
                    {
                        renderer.color = invisibleValue;
                    }
                }
            }

            foreach (SpriteRenderer renderer in midgrounds)
            {
                if (renderer.color != invisibleValue)
                {
                    renderer.color -= valueToReach;
                    if (renderer.color.a < invisibleValue.a)
                    {
                        renderer.color = invisibleValue;
                    }
                }
            }

        }
        else if (layerVar.value == 2)
        {
            foreach (SpriteRenderer renderer in backgrounds)
            {
                if (renderer.color != invisibleValue)
                {
                    renderer.color -= valueToReach;
                    if (renderer.color.a < invisibleValue.a)
                    {
                        renderer.color = invisibleValue;
                    }
                }
            }

            foreach (SpriteRenderer renderer in midgrounds)
            {
                if (renderer.color != visibleValue)
                {
                    renderer.color += valueToReach;
                    if (renderer.color.a > visibleValue.a)
                    {
                        renderer.color = visibleValue;
                    }
                }
            }
        }
        else if (layerVar.value == 3 || layerVar.value == 4)
        {
            foreach (SpriteRenderer renderer in backgrounds)
            {
                if (renderer.color != visibleValue)
                {
                    renderer.color += valueToReach;
                    if (renderer.color.a > visibleValue.a)
                    {
                        renderer.color = visibleValue;
                    }
                }
            }

            foreach (SpriteRenderer renderer in midgrounds)
            {
                if (renderer.color != visibleValue)
                {
                    renderer.color += valueToReach;
                    if (renderer.color.a > visibleValue.a)
                    {
                        renderer.color = visibleValue;
                    }
                }
            }
        }
    }
}
