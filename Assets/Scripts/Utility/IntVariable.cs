using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IntVariable", menuName = "Variables/IntVariable")]
public class IntVariable : ScriptableObject
{
    // Developer Commentary
#if UNITY_EDITOR
    [Multiline]
    public string description = "";
#endif

    // Attributes
    public int value = 0;
}
