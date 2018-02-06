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
        public Pool pool;
        #endregion

        #region Methods
        public void Initialize()
        {
            GameObject poolObject = GameObject.Find("Pool - " +bullet.name.Replace("(Clone)", ""));
            if (poolObject == null)
            {
                poolObject = new GameObject("Pool - " + bullet.name.Replace("(Clone)", ""));
                pool = poolObject.AddComponent<Pool>();
                pool.Initialize(bullet);
            }
            else
                pool = poolObject.GetComponent<Pool>();

            done = false;
        }

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
