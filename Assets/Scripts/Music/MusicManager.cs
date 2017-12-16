using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
	int m_mesureBeat;
	int m_mesure;
	bool m_isPlaying, m_initPlay;

	[SerializeField]
	string m_nameBaseLoop;
	[SerializeField]
	string m_nameBaseKick;

	void Start ()
	{
		BPM_Manager.SyncedAction += OnBeat;
		Play();
	}

	public void Play()
	{
		m_initPlay = true;
	}
	
	void OnBeat()
	{
		if(m_isPlaying)
		{
			m_mesureBeat++;

			if(m_mesureBeat == 4)
			{
				m_mesureBeat = 0;
				m_mesure++;

				if(m_mesure % 4 == 0)
				{
					AkSoundEngine.PostEvent(m_nameBaseLoop, gameObject);
					AkSoundEngine.PostEvent(m_nameBaseKick, gameObject);
					AkSoundEngine.PostEvent("Bass_mute", gameObject);
					AkSoundEngine.PostEvent("Bass_Unmute", gameObject);
				}
			}
		}

		if(m_initPlay)
		{
			AkSoundEngine.PostEvent(m_nameBaseLoop, gameObject);
			AkSoundEngine.PostEvent(m_nameBaseKick, gameObject);
			AkSoundEngine.PostEvent("Bass_mute", gameObject);

			m_isPlaying = true;
			m_initPlay = false;
		}
	}
}
