using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BackgroundReapeatable : MonoBehaviour {

	[SerializeField]
	Transform m_referencedBG;
	BoxCollider2D m_collider;
    [SerializeField]
    float offset;
	void Start () {
		m_collider = GetComponent<BoxCollider2D>();
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
        if (other.CompareTag("CenterScreenTrigger"))
            m_referencedBG.position = transform.position + new Vector3(m_collider.size.x * transform.localScale.x - offset, 0, 0);
	}
}
