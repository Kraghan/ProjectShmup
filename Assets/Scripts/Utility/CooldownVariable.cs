using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownVariable : MonoBehaviour
{
    #region Attributes
    [Header("Cooldown attributes")]
    [SerializeField]
    [Tooltip("Name of the cooldown.")]
    private string cooldownName = "Cooldown";
    [SerializeField]
    [Tooltip("Time (in seconds) to reach.")]
    private FloatVariable max;
    [SerializeField]
    [Tooltip("Time (in seconds) reached by the cooldown.")]
    private FloatVariable value;
    [SerializeField]
    [Tooltip("Increase = true: the cooldown start from 0 to the max. Else, it decrease from the max to 0.")]
    private bool increase = true;
    [SerializeField]
    [Tooltip("If the cooldown is currently active.")]
    private bool active = true;
    [SerializeField]
    [Tooltip("Restart the cooldown each time it reach the value.")]
    private bool restartCD = false;
    #endregion

    #region MonoBehaviour main methods
    // Use this for initialization
    void Start()
    {
        Utility.Cap(ref value.value, 0, max.value);
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
            return;
        value.value += ((increase) ? Time.deltaTime : -Time.deltaTime);
        Utility.Cap(ref value.value, 0, max.value);
        if (IsUp() && restartCD)
            RestartCooldown();
    }
    #endregion

    #region Methods
    public void RestartCooldown()
    {
        value.value = ((increase) ? 0 : max.value);
    }

    public bool IsUp() { return ((increase) ? ((value == max) ? true : false) : ((value.value == 0) ? true : false)); }
    public void SetIsUp(bool _isUp) { value.value = ((_isUp) ? ((increase) ? max.value : 0) : ((increase) ? 0 : max.value)); }
    #endregion

    #region Getters
    public string GetCooldownName() { return cooldownName; }
    public float GetMax() { return max.value; }
    public float GetValue() { return value.value; }
    public bool GetIncrease() { return increase; }
    public bool GetActive() { return active; }
    #endregion

    #region Setters
    public void SetCdName(string _cooldownName) { cooldownName = _cooldownName; }
    public void SetMax(float _max) { max.value = _max; Utility.Cap(ref value.value, 0, max.value); }
    public void SetValue(float _value) { Utility.Cap(ref _value, 0, max.value); value.value = _value; }
    public void SetIncrease(bool _increase) { increase = _increase; }
    public void SetActive(bool _active) { active = _active; }
    #endregion
}