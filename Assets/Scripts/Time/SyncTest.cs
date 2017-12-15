using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncTest : MonoBehaviour
{
    [SerializeField]
    Renderer m_renderer;

    void Start()
    {
        BPM_Manager.SyncedAction += Test;
    }

    void Test()
    {
        m_renderer.enabled = !m_renderer.enabled;
    }
}
