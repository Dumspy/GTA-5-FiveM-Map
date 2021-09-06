using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private WaypointController waypointController;

    private Camera mainCam;
    private readonly float[] zoomLevels = {5, 10, 15, 20, 25 };
    private readonly float[] dragSpeeds = {0.3f, 0.5f, 0.7f, 0.9f, 1 };
    private readonly float[] scaleLevels = {1, 1, 1.5f ,2, 2.5f};
    private int currentZoomLevel = 2;
    private Vector3 dragOrigin;
    private float dragSpeed = 0.8f;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        if(gameManager.cameraState == GameManager.CameraState.CameraLocked) { return; }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            UpdateZoomLevel(true);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            UpdateZoomLevel(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }
 
        if (!Input.GetMouseButton(0)) return;
 
        var pos = mainCam.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        var move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0);

        Transform transform1;
        (transform1 = transform).Translate(-move, Space.World);

        var position = transform1.position;
        position = new Vector3(Mathf.Clamp(position.x, -30, 30), Mathf.Clamp(position.y, -40, 40), -10);
        transform.position = position;
    }
     
    private void UpdateZoomLevel(bool way)
    {
        _ = way == true ? currentZoomLevel-- : currentZoomLevel++;

        currentZoomLevel = Mathf.Clamp(currentZoomLevel, 0, zoomLevels.Length-1);
        
        waypointController.SetWaypointsSize(scaleLevels[currentZoomLevel]);
        dragSpeed = dragSpeeds[currentZoomLevel];

        mainCam.orthographicSize = zoomLevels[currentZoomLevel];
    }
}
