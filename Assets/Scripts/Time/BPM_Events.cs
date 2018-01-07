using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BPM_Events : MonoBehaviour
{
	[SerializeField]
	UnityEvent m_OnBeat;

	// Use this for initialization
	void Start () {
		BPM_Manager.SyncedAction += OnBeat;
	}
	
	void OnBeat()
	{
		m_OnBeat.Invoke();
	}
}
