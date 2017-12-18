using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShmupPatternPackage
{
    [CreateAssetMenu(fileName = "New Burst", menuName = "ShmupPatternPackage/Burst", order = 2)]
    [System.Serializable]
    public class Burst : ScriptableObject
    {
        #region Attributes
        [SerializeField]
        public float spread = 0;
        [SerializeField]
        public List<Shoot> shoots = new List<Shoot>();
        #endregion

        #region Methods
        public void Fire(float burstDirection, GameObject bulletPrefab, Vector3 patternPosition, Transform bulletRepository)
        {
            foreach (Shoot shoot in shoots)
            {
                GameObject bulletGO = Instantiate(bulletPrefab, patternPosition, Quaternion.identity, bulletRepository);
                Bullet bullet = bulletGO.SecureGetComponent<Bullet>();
                bullet.Fire(shoot.speed, shoot.acceleration, (shoot.direction + burstDirection) % 360, shoot.rotation);
            }
        }
        #endregion
    }
}