using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
	int m_mesureBeat;
	int m_mesure;
	bool m_isPlaying;

	[SerializeField, Range(1,4)]
	int m_state = 0;

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
			if(m_mesureBeat == 0)
			{
				if(m_mesure % 4 == 0)
				{
					switch (m_state)
					{
						case 1:
							AkSoundEngine.PostEvent("Music_Combo_1", gameObject);
							AkSoundEngine.SetState("Music_Gameplay_1", "Music_Combo_1");
							break;
						case 2:
							AkSoundEngine.PostEvent("Music_Combo_1", gameObject);
							AkSoundEngine.SetState("Music_Gameplay_1", "Music_Combo_2");
							break;
						case 3:
							AkSoundEngine.PostEvent("Music_Combo_2", gameObject);
							AkSoundEngine.SetState("Music_Gameplay_2", "Music_Combo_3");
							break;
						case 4:
							AkSoundEngine.PostEvent("Music_Combo_2", gameObject);
							AkSoundEngine.SetState("Music_Gameplay_2", "Music_Combo_Full");
							break;
					}
				}

				m_mesureBeat = 0;
				m_mesure++;
			}

			m_mesureBeat++;
		}
	}
}
