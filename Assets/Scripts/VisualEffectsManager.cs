using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectsManager : MonoBehaviour {

	[SerializeField]
	IntVariable m_state;

	[SerializeField]
	float m_speed;

	[SerializeField]
	Transform[] m_serial;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		for (int tID = 0; tID < m_serial.Length; tID++)
		{
			m_serial[tID].localPosition = Vector3.MoveTowards(m_serial[tID].localPosition, tID+1 == m_state.value ? Vector2.zero : Vector2.one, m_speed);
		}
	}
}
