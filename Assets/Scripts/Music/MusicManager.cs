using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
	int m_mesureBeat;
	int m_mesure;

	[SerializeField]
	string m_nameBaseLoop;

	void Start ()
	{
		BPM_Manager.SyncedAction += OnBeat;
		AkSoundEngine.PostEvent(m_nameBaseLoop, gameObject);
	}
	
	void OnBeat()
	{
		m_mesureBeat++;

		if(m_mesureBeat == 4)
		{
			m_mesureBeat = 0;
			m_mesure++;

			if(m_mesure % 4 == 0)
				AkSoundEngine.PostEvent(m_nameBaseLoop, gameObject);
		}
	}
}
