using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Burst", menuName = "Shmup/Burst")]
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
        foreach(Shoot shoot in shoots)
        {
            Bullet bullet = Instantiate(bulletPrefab, patternPosition, Quaternion.identity, bulletRepository).GetComponent<Bullet>();
            bullet.Fire(shoot.speed, shoot.acceleration, (shoot.direction + burstDirection) %360, shoot.rotation);
        }
    }
    #endregion
}
