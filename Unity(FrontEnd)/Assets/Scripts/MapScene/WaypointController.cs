using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaypointController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private WebRequestManager webRequestManager;
    [SerializeField] private GameObject waypointPrefab;

    private WaypointList waypoints;

    [SerializeField] private List<Waypoint> waypointList;

    [SerializeField] private List<GameObject> waypointObjects = new List<GameObject>();

    // Start is called before the first frame update
    private void Start()
    {
        Invoke(nameof(GetWaypointsInvoke), 0.25f);
    }

    private void GetWaypointsInvoke()
    {
        StartCoroutine(webRequestManager.GetWaypoints());        
    }
    
    public void GetWaypoint()
    {
        waypoints = webRequestManager.waypoints;
        SpawnWaypoints();
    }

    private void SpawnWaypoints()
    {
        waypointList = waypoints.Values.ToList();

        foreach (var waypoint in waypointList)
        {
            var wpPrefab = Instantiate(waypointPrefab);
            waypointObjects.Add(wpPrefab);
            wpPrefab.GetComponent<WaypointSetupHandler>().Setup(waypoint);
        }
    }

    public void SpawnWaypoint(Waypoint wp)
    {
        var wpPrefab = Instantiate(waypointPrefab);
        waypointList.Add(wp);
        waypointObjects.Add(wpPrefab);
        wpPrefab.GetComponent<WaypointSetupHandler>().Setup(wp);
    }

    public void SpawnWaypointForWebReqest()
    {
        var wpPrefab = Instantiate(waypointPrefab);
        waypointList.Add(webRequestManager.latestWaypoint);
        waypointObjects.Add(wpPrefab);
        wpPrefab.GetComponent<WaypointSetupHandler>().Setup(webRequestManager.latestWaypoint);        
    }

    public void UpdateWaypoint(Waypoint wp)
    {
        var i = 0;
        foreach (var waypoint in waypointList)
        {
            if (waypoint._id == wp._id)
            {
                waypointObjects[i].GetComponent<WaypointSetupHandler>().WaypointUpdate(wp);
                return;
            }

            i++;
        }
    }

    public void DeleteWaypoint(Waypoint wp)
    {
        var i = 0;
        foreach (var waypoint in waypointList)
        {
            if (waypoint._id == wp._id)
            {
                waypointList.RemoveAt(i);
                Destroy(waypointObjects[i]);
                waypointObjects.RemoveAt(i);
                return;
            }

            i++;
        }
    }

    public void SetWaypointsSize(float size)
    {
        if (gameManager.cameraState == GameManager.CameraState.CameraLocked) return;
        foreach (var waypoint in waypointObjects.TakeWhile(waypoint => waypoint != null))
        {
            waypoint.transform.localScale = new Vector3(size, size);
        }
    }
}