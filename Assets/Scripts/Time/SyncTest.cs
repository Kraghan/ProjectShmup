using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SyncTest : MonoBehaviour
{
    [SerializeField]
    RawImage m_renderer;

    void Start()
    {
        BPM_Manager.SyncedAction += Test;
    }

    void Test()
    {
        m_renderer.enabled = !m_renderer.enabled;
    }
}
