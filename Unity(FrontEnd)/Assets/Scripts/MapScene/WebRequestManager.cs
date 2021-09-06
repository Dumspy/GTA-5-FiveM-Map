using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequestManager : MonoBehaviour
{
    public WaypointList waypoints;
    public Waypoint latestWaypoint;

    public WaypointController waypointController;

    private DataContainer dataContainer;

    private void Start()
    {
        dataContainer = FindObjectOfType<DataContainer>();
    }

    public IEnumerator UpdateWaypoint(Waypoint wp)
    {
        var form = new WWWForm();

        form.AddField("id", wp._id);
        form.AddField("name", wp.name);
        form.AddField("posX", wp.pos.x + "");
        form.AddField("posY", wp.pos.y + "");
        form.AddField("iconID", wp.iconId);
        form.AddField("description", wp.description);
        form.AddField("r", wp.color.r);
        form.AddField("g", wp.color.g);
        form.AddField("b", wp.color.b);
        form.AddField("key",dataContainer.apiKey);

        using (var www = UnityWebRequest.Post("https://dispyapi.auxera.net/api/map/updateWaypoint", form))
        {
            yield return www.SendWebRequest();
        }
    }
    public IEnumerator DeleteWaypoint(string id)
    {
        var form = new WWWForm();
        form.AddField("id", id);

        using (var www = UnityWebRequest.Delete("https://dispyapi.auxera.net/api/map/deleteWaypoint&id=" + id+"&key="+dataContainer.apiKey))
        {
            yield return www.SendWebRequest();
        }
    }

    public IEnumerator GetWaypoints()
    {
        using (var webRequest = UnityWebRequest.Get("https://dispyapi.auxera.net/api/map/getWaypoints&key="+dataContainer.apiKey))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                var json = webRequest.downloadHandler.text;
                waypoints = JsonUtility.FromJson<WaypointList>(json);
                waypointController.GetWaypoint();
            }
        }
    }

    public IEnumerator PostWaypoint(Waypoint wp)
    {
        var form = new WWWForm();
        form.AddField("name", wp.name);
        form.AddField("posX", wp.pos.x + "");
        form.AddField("posY", wp.pos.y + "");
        form.AddField("iconID", wp.iconId);
        form.AddField("description", wp.description);
        form.AddField("r", wp.color.r);
        form.AddField("g", wp.color.g);
        form.AddField("b", wp.color.b);
        form.AddField("key",dataContainer.apiKey);

        using (var www = UnityWebRequest.Post("https://dispyapi.auxera.net/api/map/createWaypoint", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                wp._id = www.downloadHandler.text;
                wp._id = wp._id.Substring(1, wp._id.Length-2);
                latestWaypoint = wp;
                waypointController.SpawnWaypointForWebReqest();
            }
        }
    }
}