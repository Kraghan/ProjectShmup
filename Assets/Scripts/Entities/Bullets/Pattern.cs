using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RepetitionsMode
{
    Infinite,
    Finite
}

[CreateAssetMenu(fileName = "Pattern", menuName = "Shmup/Pattern")]
[System.Serializable]
public class Pattern : ScriptableObject
{
    #region Attributes
    [SerializeField]
    public RepetitionsMode selectedRepetitionsMode = RepetitionsMode.Infinite;
    [Tooltip("Duration of the pattern (in seconds)")]
    [SerializeField]
    public float duration = 1;
    [Tooltip("Number of repetitions of the pattern (-1 = infinite | 0 = one cycle | 1 = two cycles (one repetition) | etc...)")]
    [SerializeField]
    public int cycles = -1;
    [Tooltip("Bursts timings")]
    [SerializeField]
    public List<BurstTiming> bursts;

    private Transform bulletRepository;
    private float time = 0;
    #endregion

    #region OldMonoBehaviour main methods
    public void PatternSetup()
    {
        bulletRepository = GameObject.FindGameObjectWithTag("BulletRepository").transform;
    }
   
    public void PatternUpdate(GameObject go)
    {
        time += Time.deltaTime;
        Utility.Cap(ref time, 0, duration);
        foreach(BurstTiming burstTiming in bursts)
        {
            if (burstTiming.timing <= time && !burstTiming.IsDone())
            {
                if(burstTiming.bullet != null)
                {
                    burstTiming.burst.Fire(burstTiming.bullet, go.transform.position, bulletRepository.transform);
                    burstTiming.Done();
                }
            }
        }
        if(time == duration)
        {
            if (cycles == 0)
                Destroy(go);
            else
            {
                if (cycles > 0)
                    cycles--;
                foreach (BurstTiming burstTiming in bursts)
                    burstTiming.Reset();
                time = 0;
            }
        }

    }
    #endregion

    #region Methods
    #endregion

    #region Getters
    #endregion

    #region Setters
    #endregion
}
