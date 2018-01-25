using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTPCSlider : MonoBehaviour {

	[SerializeField]
	string m_rtpc;

	public void CallRTPC(float value)
	{
		AkSoundEngine.SetRTPCValue(m_rtpc, value*100);
	}
}
