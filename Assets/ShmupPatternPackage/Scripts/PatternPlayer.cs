using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShmupPatternPackage
{
    public class PatternPlayer : MonoBehaviour
    {

        [SerializeField]
        public Pattern pattern;

        // Use this for initialization
        void Start()
        {
            if (pattern == null)
                Debug.LogError("Pattern not set !");
            pattern.PatternSetup();
        }

        // Update is called once per frame
        void Update()
        {
            float targetDirection = 0;
            pattern.PatternUpdate(gameObject, targetDirection);
        }
    }
}