using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    #region Attributes
    [SerializeField]
    private float scrollSpeed = 2.5f;

    #endregion

    #region Monobehaviour
    // Use this for initialization
    void Start ()
    {
        
    }

	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x + scrollSpeed * Time.deltaTime, pos.y, pos.z);
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
           
    }
    #endregion
}
