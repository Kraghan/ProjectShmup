using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
	bool m_isPlaying;

	int m_nextTransitionCD = -1;

	bool launched = false;

	[SerializeField]
	IntVariable m_state;
	int m_currentState = 1;
	int State {
		set{
			m_nextTransitionCD = 0;

			if (m_currentState < value)
				AkSoundEngine.PostEvent("LayerUp", gameObject);
			else if(m_currentState > value)
				AkSoundEngine.PostEvent("LayerDown", gameObject);
				
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

		if(!launched)
		{
			AkSoundEngine.PostEvent("Music_Gameplay", gameObject);
			launched = true;
		}
		AkSoundEngine.SetState("Music_Gameplay_1", "Music_Combo_1");
	}
	
	void OnBeat()
	{
		if(m_isPlaying)
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

				m_nextTransitionCD = 16;
			}
		}
	}
}
