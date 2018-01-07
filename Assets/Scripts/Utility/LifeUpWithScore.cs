using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUpWithScore : MonoBehaviour {

    [SerializeField]
    private FloatVariable scoreVariable;

    [SerializeField]
    private FloatVariable lifeVariable;

    [SerializeField]
    private FloatVariable maxLifeVariable;

    [SerializeField]
    private int scoreTreshold;

    private int lastScoreWeOfferLife;

    private void Start()
    {
        lastScoreWeOfferLife = 0;
    }

    // Update is called once per frame
    void Update () {
		if(scoreVariable.value - lastScoreWeOfferLife >= scoreTreshold)
        {
            if (lifeVariable.value < maxLifeVariable.value)
                lifeVariable.value++;

            lastScoreWeOfferLife = (int)scoreVariable.value;
        }
	}
}
