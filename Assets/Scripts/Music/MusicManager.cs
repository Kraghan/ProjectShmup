using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
	int m_beat = 0;
	int m_loopLenght = 8;
	bool m_isPlaying;

	[SerializeField, Range(1,4)]
	int m_state = 0;
	int m_oldState = -1;

	void Start ()
	{
		BPM_Manager.SyncedAction += OnBeat;
	}

	public void Play()
	{
		m_isPlaying = true;
		
	}
	
	void OnBeat()
	{
		if(m_isPlaying)
		{
			if(m_beat == 0)
			{
				m_beat = 4 * m_loopLenght;

				AkSoundEngine.PostEvent("Music_Gameplay", gameObject);
			}

			m_beat--;

			if(m_oldState != m_state)
			{
				switch (m_state)
				{
					case 1:
						AkSoundEngine.SetState("Music_Gameplay_1", "Music_Combo_1");
						break;
					case 2:
						AkSoundEngine.SetState("Music_Gameplay_1", "Music_Combo_2");
						break;
					case 3:
						AkSoundEngine.SetState("Music_Gameplay_1", "Music_Combo_3");
						break;
					case 4:
						AkSoundEngine.SetState("Music_Gameplay_1", "Music_Combo_Full");
						break;
				}

				m_beat = 4 * m_loopLenght;
				m_oldState = m_state;
			}
				
		}
	}
}
