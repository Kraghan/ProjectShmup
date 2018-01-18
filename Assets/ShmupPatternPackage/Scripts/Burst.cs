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
        public void Fire(PatternSource _source, float burstDirection, float targetDirection, GameObject bulletPrefab, Vector3 patternPosition, Transform bulletRepository)
        {
            foreach (Shoot shoot in shoots)
            {
                GameObject bulletGO = Instantiate(bulletPrefab, patternPosition, Quaternion.identity, bulletRepository);
                if (_source == PatternSource.Enemy)
                    bulletGO.tag = "EnemyBullet";
                else if (_source == PatternSource.Player)
                    bulletGO.tag = "PlayerBullet";
                if (_source == PatternSource.Enemy)
                    bulletGO.layer = 13;
                else if (_source == PatternSource.Player)
                    bulletGO.layer = 12;
                Bullet bullet = bulletGO.SecureGetComponent<Bullet>();
                bullet.Fire(shoot.speed, shoot.acceleration, (shoot.direction + burstDirection + targetDirection) % 360, shoot.rotation);
            }
        }
        #endregion
    }
}