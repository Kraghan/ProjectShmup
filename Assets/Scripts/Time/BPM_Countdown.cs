using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BPM_Countdown : BPM_Events
{
	[SerializeField]
	UnityEvent m_OnEndBeat;
	[SerializeField]
	int m_count;

	[SerializeField]
	Text m_syncText;

	public override void OnBeat()
	{
		if(m_count != 0)
		{
			m_OnBeat.Invoke();

			m_count--;
			m_syncText.text = m_count.ToString();

			if(m_count == 0)
			{
				m_OnEndBeat.Invoke();
				BPM_Manager.SyncedAction -= OnBeat;
				this.enabled = false;
			}
		}
	}
}
