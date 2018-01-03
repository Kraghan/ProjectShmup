using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShmupPatternPackage
{
    public enum PatternSource
    {
        Enemy,
        Player
    }

    public class PatternPlayer : MonoBehaviour
    {
        [SerializeField]
        private PatternSource source = PatternSource.Enemy;
        [SerializeField]
        public Pattern pattern;
        private float targetUpdate = 0.25f;
        private float elapsedTime = 0;
        private float targetDirection = 0;

        // Use this for initialization
        void Start()
        {
            if (pattern == null)
                Debug.LogError("Pattern not set !");
            pattern.PatternSetup();
            TargetDirectionUpdate();
        }

        // Update is called once per frame
        void Update()
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime > targetUpdate)
            {
                TargetDirectionUpdate();
                elapsedTime = 0;
            }
            pattern.PatternUpdate(gameObject, source, targetDirection);
        }

        private void TargetDirectionUpdate()
        {
            if(source == PatternSource.Enemy)
            {
                GameObject target = GameObject.FindGameObjectWithTag("Player");
                if(target != null)
                {
                    float MyPositionX = transform.position.x;
                    float MyPositionZ = transform.position.y;
                    float TargetPositionX = target.transform.position.x;
                    float TargetPositionZ = target.transform.position.y;
                    float value = (float)((System.Math.Atan2((MyPositionX - TargetPositionX), (MyPositionZ - TargetPositionZ)) / System.Math.PI) * 180f);
                    value += 90;
                    while (value < 0) value += 360f;
                    targetDirection = value;
                }
                
            }
            
        }
    }
}