using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoolVariable", menuName = "Variables/BoolVariable")]
public class BoolVariable : ScriptableObject
{
    // Developer Commentary
#if UNITY_EDITOR
    [Multiline]
    public string description = "";
#endif

    // Attributes
    public bool state = true;
}
