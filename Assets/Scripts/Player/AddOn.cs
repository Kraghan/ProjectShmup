using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOn : MonoBehaviour
{
    #region Attributes
    [SerializeField]
    pri
    private Rigidbody2D rgbd2D;
    private float horizontalSpeed;
    private float focusSpeedMultiplicator;
    private float verticalSpeed;
    private bool assignedValues = false;
    private GameObject player;
    private bool needToReassignPos = true;
    #endregion

    // Use this for initialization
    void Start () {
        rgbd2D = GetComponent<Rigidbody2D>();
        
	}
	
	// Update is called once per frame
	void Update () {
        ManageSpeed();
    }

    private void FindPlayerAndAssignValues()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            horizontalSpeed = playerController.GetHorizontalSpeed();
            verticalSpeed = playerController.GetVerticalSpeed();
            focusSpeedMultiplicator = playerController.GetFocusSpeedMultiplicator();
            ReassignPos();
            assignedValues = true;
        }
    }

    private void ReassignPos()
    {
        transform.position = player.transform.position;
        needToReassignPos = false;
    }

    private void ManageSpeed()
    {
        if (assignedValues == false)
            FindPlayerAndAssignValues();
        if (player == null)
        {
            needToReassignPos = true;
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (player != null && needToReassignPos)
            ReassignPos();
        bool focus = (Input.GetButton("Focus"));

        Vector2 positionOnScreen = Camera.main.WorldToScreenPoint(transform.position);

        rgbd2D.velocity = new Vector2(Time.deltaTime * horizontalSpeed * (focus ? focusSpeedMultiplicator : 1) * Input.GetAxis("Horizontal"), rgbd2D.velocity.y);
        rgbd2D.velocity = new Vector2(rgbd2D.velocity.x, Time.deltaTime * verticalSpeed * (focus ? focusSpeedMultiplicator : 1) * Input.GetAxis("Vertical"));
    }
}
