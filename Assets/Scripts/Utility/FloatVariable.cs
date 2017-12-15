using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatVariable", menuName = "Variables/FloatVariable")]
public class FloatVariable : ScriptableObject
{
    // Developer Commentary
#if UNITY_EDITOR
    [Multiline]
    public string description = "";
#endif

    // Attributes
    public float value = 0;
}
