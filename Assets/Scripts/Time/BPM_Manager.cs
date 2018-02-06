using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Threading;

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
	static double m_timeLastBeat = 0;
	static double m_timeSinceLastBeat = 0;

	void Start ()
	{
		m_beatDuration = 60d / m_BPM * 10000000;

		m_timeLastBeat = System.DateTime.Now.Ticks;

		_thread = new Thread(ThreadedWork);
        _thread.Start();
	}

	public static bool IsOnBeat(float error)
	{
		error *= 10000000;

		if(m_timeSinceLastBeat < error)
			return true;
		else if(m_timeSinceLastBeat > (m_beatDuration - error))
			return true;
		else
			return false;
	}

	void Update()
	{
		// Beat
		if(m_timeSinceLastBeat > m_beatDuration)
		{
			m_timeLastBeat = System.DateTime.Now.Ticks - (m_timeSinceLastBeat - m_beatDuration);
			m_syncList.Invoke();
		}
	}

	bool _threadRunning;
    Thread _thread;
    void ThreadedWork()
    {
        _threadRunning = true;
        bool workDone = false;

        while(_threadRunning && !workDone)
            m_timeSinceLastBeat = System.DateTime.Now.Ticks - m_timeLastBeat;

        _threadRunning = false;
    }

    void OnDisable()
    {
        if(_threadRunning)
        {
            _threadRunning = false;
            _thread.Join();
        }
    }
}
