using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
	int m_beat;
	int m_loopLenght = 8;
	bool m_isPlaying;

	[SerializeField]
	IntVariable m_state;
	int m_currentState = -1;
	int State {
		set{
			m_currentState = value;

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
		State = m_state.value;
	}
	
	void OnBeat()
	{
		if(m_isPlaying)
		{
			if (m_beat == 4*m_loopLenght)
			{
				m_beat = 1;

				if(State == 1)
				{
					AkSoundEngine.PostEvent("Music_Gameplay", gameObject);
					AkSoundEngine.SetState("Music_Gameplay_1", "Music_Combo_1");
				}
			}
			else
				m_beat++;

			if(State != m_state.value)
			{
				m_beat++;
				State = m_state.value;
			}
		}
	}
}
