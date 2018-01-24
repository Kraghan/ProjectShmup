using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
	int m_beat;
	int m_loopLenght = 8;
	bool m_isPlaying;

	int m_nextTransitionCD = -1;

	[SerializeField]
	IntVariable m_state;
	int m_currentState = 1;
	int State {
		set{
			if(m_currentState == 2 && value == 3)
			{
				AkSoundEngine.SetState("Music_Gameplay_1", "Music_Transition");
				m_nextTransitionCD = 8;
			}
			else
				m_nextTransitionCD = 0;

			m_currentState = value;
		}

		get
		{
			return m_currentState;
		}
	}

	void Start ()
	{
		BPM_Manager.SyncedAction += OnBeat;
	}

	public void Play()
	{
		m_isPlaying = true;

		m_beat = 4*m_loopLenght;

		AkSoundEngine.PostEvent("Music_Gameplay", gameObject);
		AkSoundEngine.SetState("Music_Gameplay_1", "Music_Combo_1");
	}
	
	void OnBeat()
	{
		if(m_isPlaying)
		{
			// Relaunch main beat
			if (m_beat == 4*m_loopLenght)
			{
				m_beat = 1;
			}
			else
			{
				if(State != m_state.value)
				State = m_state.value;

				if(m_nextTransitionCD > 0)
					m_nextTransitionCD--;
				else if(m_nextTransitionCD == 0)
				{
					switch (m_currentState)
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

					// m_beat = 4*m_loopLenght;
					m_nextTransitionCD = -1;
				}
			}

			m_beat++;
		}
	}
}
