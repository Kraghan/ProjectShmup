using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeVariable : MonoBehaviour
{
    #region Attributes
    [Header("Gauge attributes")]
    [SerializeField]
    [Tooltip("Name of the gauge. (Ex: \"Health\", \"Mana\"...)")]
    private string gaugeName = "Gauge";
    [SerializeField]
    [Tooltip("Maximum value of the gauge.")]
    private FloatVariable max;
    [SerializeField]
    [Tooltip("Starting value of the gauge.")]
    private FloatVariable value;
    [SerializeField]
    [Tooltip("Amount added/soustracted to the value each second.")]
    private FloatVariable modifierAmount;
    [SerializeField]
    [Tooltip("If the gain or loose per second is currently effective")]
    private bool modifierEnabled = true;
    #endregion

    #region MonoBehaviour main methods
    // Use this for initialization
    void Start()
    {
        if (max == null || value == null || (modifierAmount == null && modifierEnabled))
            Debug.LogError("Please set the FloatVariables of the gauge \"" + name + "\".");
        Utility.Cap(ref value.value, 0, max.value);
    }

    // Update is called once per frame
    void Update()
    {
        if (!modifierEnabled)
            return;
        value.value += modifierAmount.value * Time.deltaTime;
        Utility.Cap(ref value.value, 0, max.value);
    }
    #endregion

    #region Methods
    public bool IsFull() { return ((value.value == max.value) ? true : false); }
    public bool IsEmpty() { return ((value.value == 0) ? true : false); }
    public void SetFull() { value.value = max.value; }
    public void SetEmpty() { value.value = 0; }
    public void AddValue(float amount) { float newValue = value.value + amount; value.value = Utility.Cap(ref newValue, 0, max.value); }
    #endregion

    #region Getters
    public string GetGaugeName() { return gaugeName; }

    public float GetMax() { return max.value; }
    public float GetValue() { return value.value; }
    public float GetModifierAmount() { return modifierAmount.value; }

    public FloatVariable GetMaxFV() { return max; }
    public FloatVariable GetValueFV() { return value; }
    public FloatVariable GetModifierAmountFV() { return modifierAmount; }

    public bool GetModifierEnabled() { return modifierEnabled; }
    #endregion

    #region Setters
    public void SetGaugeName(string _gaugeName) { gaugeName = _gaugeName; }

    public void SetMax(float _max) { max.value = _max; Utility.Cap(ref value.value, 0, max.value); }
    public void SetValue(float _value) { Utility.Cap(ref _value, 0, max.value); value.value = _value; }
    public void SetModifierAmount(float _modifierAmount) { modifierAmount.value = _modifierAmount; }

    public void SetMaxFV(FloatVariable _max) { max = _max; Utility.Cap(ref value.value, 0, max.value); }
    public void SetValueFV(FloatVariable _value) { Utility.Cap(ref _value.value, 0, max.value); value = _value; }
    public void SetModifierAmountFV(FloatVariable _modifierAmount) { modifierAmount = _modifierAmount; }

    public void SetModifierEnabled(bool _modifierEnabled) {
        if (_modifierEnabled && modifierAmount == null)
            Debug.LogError("Please set the FloatVariable ModifierAmount of the gauge \"" + name + "\".");
        modifierEnabled = _modifierEnabled;
    }
    #endregion
}
