using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShmupPatternPackage
{
    [System.Serializable]
    public class BurstTiming
    {
        #region Attributes
        [SerializeField]
        public float timing = 0;
        [SerializeField]
        public Burst burst;
        [SerializeField]
        public GameObject bullet;
        [SerializeField]
        public float direction;
        [SerializeField]
        public AimMode aimMode = AimMode.WorldSpace;
        [SerializeField]
        public bool targetted;

        private bool done = false;
        #endregion

        #region Methods
        public void Done()
        {
            done = true;
        }

        public void Reset()
        {
            done = false;
        }
        #endregion

        #region Getters
        public bool IsDone()
        {
            return done;
        }
        #endregion
    }
}
