using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BPM_Manager : MonoBehaviour
{
	static UnityAction m_syncList;

	public static UnityAction SyncedAction
	{
		set
		{
			m_syncList = value;
		}

		get
		{
			return m_syncList;
		}
	}

	[SerializeField, Range(100,200)]
	int m_BPM = 128;

	float m_beatDuration;
	float m_timeSinceLastBeat = 0;

	void Start ()
	{
		m_beatDuration = 60f / m_BPM;
		print(m_beatDuration);
	}
	
	void Update ()
	{
		m_timeSinceLastBeat += Time.deltaTime;

		// Beat
		if(m_timeSinceLastBeat > m_beatDuration)
		{
			m_timeSinceLastBeat = 0;
			m_syncList.Invoke();
		}
	}
}
