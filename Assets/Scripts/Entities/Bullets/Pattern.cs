using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    #region Attributes
    [Tooltip("Prefab of the bullets fired")]
    public GameObject bullet;
    [Tooltip("Duration of the pattern (in seconds)")]
    [SerializeField]
    private float duration = 1;
    [Tooltip("Number of repetitions of the pattern (-1 = infinite | 0 = one cycle | 1 = two cycles (one repetition) | etc...)")]
    [SerializeField]
    private float repetitions = -1;
    [Tooltip("Bursts timings")]
    [SerializeField]
    private BurstTiming[] bursts;

    private Transform bulletRepository;
    private float time = 0;
    #endregion

    #region MonoBehaviour main methods
    // Use this for initialization
    void Start()
    {
        bulletRepository = GameObject.FindGameObjectWithTag("BulletRepository").transform;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        Utility.Cap(ref time, 0, duration);
        foreach(BurstTiming burstTiming in bursts)
        {
            if (burstTiming.timing <= time && !burstTiming.IsDone())
            {
                burstTiming.burst.Fire(bullet, transform.position, bulletRepository.transform);
                burstTiming.Done();
            }
        }
        if(time == duration)
        {
            if (repetitions == 0)
                Destroy(gameObject);
            else
            {
                if (repetitions > 0)
                    repetitions--;
                foreach (BurstTiming burstTiming in bursts)
                    burstTiming.Reset();
                time = 0;
            }
        }

    }
    #endregion

    #region Methods
    public void ResetCooldown()
    {
        time = 0;
    }
    #endregion

    #region Getters
    #endregion

    #region Setters
    #endregion
}
