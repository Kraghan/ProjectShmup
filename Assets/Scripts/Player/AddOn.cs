using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOn : MonoBehaviour
{
    #region Attributes
    [SerializeField]
    private float addonSpeed = 1.6f;
    [SerializeField]
    private Vector3 offset;
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

        if (player != null)
        {
            Vector3 targetPos = player.transform.position + offset;
            float horizontalInput = targetPos.x - transform.position.x;
            float verticalInput = targetPos.y - transform.position.y;
            if (Vector3.Distance(transform.position, targetPos) < 0.2f)
            {
                horizontalInput = 0;
                verticalInput = 0;
            }
            Utility.FloatCap(ref horizontalInput, -1, 1);
            Utility.FloatCap(ref verticalInput, -1, 1);
            rgbd2D.velocity = addonSpeed * new Vector2(Time.deltaTime * horizontalSpeed * (focus ? focusSpeedMultiplicator : 1) * horizontalInput, rgbd2D.velocity.y);
            rgbd2D.velocity = addonSpeed * new Vector2(rgbd2D.velocity.x, Time.deltaTime * verticalSpeed * (focus ? focusSpeedMultiplicator : 1) * verticalInput);
        }
    }
}
