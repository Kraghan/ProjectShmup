using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class WaypointDeplacement : MonoBehaviour
{

    #region Attributes
    [SerializeField]
    private int currentWaypointIndex = 0;
    [SerializeField]
    private float reachRadius = 0.2f;
    [SerializeField]
    private WaypointCircuit wc;
    [SerializeField]
    private bool teleportToBeginningWhenFinished = false;

    private float currentTimeToStop;
    private bool stop = false;
    #endregion

    #region Monobehaviour
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentTimeToStop);
        if (wc.Waypoints.Length == 0 || stop)
            return;
        if (currentTimeToStop <= 0)
        {
            MoveTo(wc.Waypoints[currentWaypointIndex].position);
            if (ReachWaypoint(wc.Waypoints[currentWaypointIndex]))
            {
                if (currentWaypointIndex + 1 < wc.Waypoints.Length)
                    currentWaypointIndex++;
                else
                    currentWaypointIndex = 0;
                /*if (teleportToBeginningWhenFinished)
                {
                    gameObject.SetActive(false);
                    transform.position = wc.Waypoints[currentWaypointIndex].position;
                    if (currentWaypointIndex + 1 < wc.Waypoints.Length)
                        currentWaypointIndex++;
                    else
                        currentWaypointIndex = 0;
                    gameObject.SetActive(true);
                }*/
                currentTimeToStop = (float)wc.Waypoints[currentWaypointIndex - 1 >= 0 ? currentWaypointIndex - 1 : wc.Waypoints.Length - 1].gameObject.GetComponent<WaypointCaracteristics>().GetTimeToStop();
            }
        }
        else
        {
            currentTimeToStop -= Time.deltaTime;
        }
    }
    #endregion

    #region Methods
    private void MoveTo(Vector3 target)
    {
        float speed = wc.Waypoints[currentWaypointIndex].gameObject.GetComponent<WaypointCaracteristics>().GetSpeedToReach();
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private bool ReachWaypoint(Transform waypoint)
    {
        if (Mathf.Abs(Vector3.Distance(transform.position, wc.Waypoints[currentWaypointIndex].position)) <= reachRadius)
            return true;
        else
            return false;
    }

    public void SetCurrentWaypointIndex(int index)
    {
        currentWaypointIndex = index;
    }

    public int GetCurrentWaypointIndex()
    {
        return currentWaypointIndex;
    }

    public void SetStop(bool stop_state)
    {
        stop = stop_state;
    }

    public void SetPattern(WaypointCircuit circuit)
    {
        wc = circuit;
    }
    #endregion
}
