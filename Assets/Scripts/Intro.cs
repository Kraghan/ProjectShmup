using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Intro : MonoBehaviour {

	static bool m_firstTime = true;

	void Start () {
		if(m_firstTime)
		{
			m_firstTime = false;
			Application.LoadLevel("Intro");
		}
		
	}

	public void Load()
	{
		Application.LoadLevel("Title Screen");
	}
}
