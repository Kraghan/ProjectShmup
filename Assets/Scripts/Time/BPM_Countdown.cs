using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BPM_Countdown : BPM_Events
{
	[SerializeField]
	UnityEvent m_OnEndBeat;
	[SerializeField]
	int m_count;

	public override void OnBeat()
	{
		if(m_count != 0)
		{
			m_OnBeat.Invoke();

			m_count--;
			
			if(m_count == 0)
				m_OnEndBeat.Invoke();
		}
	}
}
