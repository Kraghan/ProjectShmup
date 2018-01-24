using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerCalculator : MonoBehaviour {
    [SerializeField]
    private int[] m_comboToPassNextLevel = new int[4];
    [SerializeField]
    private IntVariable m_layerVar;
    [SerializeField]
    private FloatVariable m_comboVar;

    // Update is called once per frame
    void Update ()
    {
        if (m_comboVar.value >= m_comboToPassNextLevel.Length)
            m_layerVar.value = m_comboToPassNextLevel.Length - 1;
        else
        {
            m_layerVar.value = 1;
            for (int i = 0; i < m_comboToPassNextLevel.Length; ++i)
            {
                if (m_comboToPassNextLevel[i] <= m_comboVar.value)
                    m_layerVar.value = i + 1;
            }
        }
	}
}
