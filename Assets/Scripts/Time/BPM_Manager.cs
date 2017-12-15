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

	static double m_beatDuration;
	static float m_timeSinceLastBeat = 0;

	void Start ()
	{
		m_beatDuration = 60d / m_BPM;
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

	public static bool IsOnBeat(float error)
	{
		if(m_timeSinceLastBeat < error)
			return true;
		else if(m_timeSinceLastBeat > (m_beatDuration - error))
			return true;
		else
			return false;
	}
}
