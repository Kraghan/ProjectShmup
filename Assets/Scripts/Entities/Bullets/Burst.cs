using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Burst", menuName = "Shmup/Burst")]
[System.Serializable]
public class Burst : ScriptableObject
{
    #region Attributes
    public Shoot[] shoots;
    #endregion

    #region Methods
    public void Fire(GameObject bulletPrefab, Transform patternTransform)
    {
        foreach(Shoot shoot in shoots)
        {
            Bullet bullet = Instantiate(bulletPrefab, patternTransform.position, Quaternion.identity, null).GetComponent<Bullet>();
            bullet.Fire(shoot.speed, shoot.acceleration, shoot.direction, shoot.rotation);
        }
    }
    #endregion
}
