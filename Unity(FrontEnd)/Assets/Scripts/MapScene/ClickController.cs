using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ClickController : MonoBehaviour
{
    [SerializeField] private UIRaycast uiRaycast;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject contextMenu;
    [SerializeField] private GameObject editMenu;
    [SerializeField] private GameObject redDot;
    [SerializeField] private GameObject actuallyTheEditMenu;
    public Vector3 posClicked;
    private Camera _camera;
    
    public enum MenuStates { None, ContextMenu, EditMenu};
    public MenuStates menuState = MenuStates.None;

    // Start is called before the first frame update
    private void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && menuState==MenuStates.None)
        {
            posClicked = Input.mousePosition;
            menuState = MenuStates.ContextMenu;
            contextMenu.SetActive(true);
            contextMenu.transform.position = posClicked;
            var localPosition = contextMenu.transform.localPosition;
            var pos = new Vector3(Mathf.Clamp(localPosition.x+170, -642,642), Mathf.Clamp(localPosition.y-207.5f, -255, 255));
            contextMenu.transform.localPosition = pos;
            if (_camera != null) 
                posClicked = _camera.ScreenToWorldPoint(posClicked);
            posClicked.z = 0;
            redDot.SetActive(true);
            redDot.transform.position = posClicked;
            
            gameManager.cameraState = GameManager.CameraState.CameraLocked;
        }else if (Input.GetMouseButtonDown(0))
        {
            var antiCunt = false;
            if (menuState == MenuStates.None)
            {
                posClicked = Input.mousePosition;
                var mousePos = _camera.ScreenToWorldPoint(posClicked);
                var mousePos2D = new Vector2(mousePos.x, mousePos.y);
                
                var hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null && hit.transform.CompareTag("Waypoint"))
                {
                    menuState = MenuStates.EditMenu;
                    editMenu.SetActive(true);
                    editMenu.transform.position = posClicked;
                    var localPosition = editMenu.transform.localPosition;
                    var pos = new Vector3(Mathf.Clamp(localPosition.x+100, -642,642), Mathf.Clamp(localPosition.y-127.5f, -255, 255));
                    editMenu.transform.localPosition = pos;
                    editMenu.GetComponent<InputDataEditMenu>().Setup(hit.transform.gameObject.GetComponent<WaypointSetupHandler>().myData);
                    gameManager.cameraState = GameManager.CameraState.CameraLocked;
                }
                antiCunt = true;
            }

            if (!uiRaycast.DoUIRaycast()) return;
            switch (menuState)
            {
                case MenuStates.ContextMenu:
                    ContextClose();
                    break;
                case MenuStates.EditMenu:
                    if (antiCunt)
                    {
                        antiCunt = false;
                    }
                    else
                    {
                        EditClose();                        
                    }
                    break;
            }
        }
    }

    public void ContextClose()
    {
        menuState = MenuStates.None;
        gameManager.cameraState = GameManager.CameraState.CameraUnlocked;
        contextMenu.SetActive(false);
        redDot.SetActive(false);
    }

    public void EditClose()
    {
        menuState = MenuStates.None;
        gameManager.cameraState = GameManager.CameraState.CameraUnlocked;
        editMenu.SetActive(false);
        actuallyTheEditMenu.SetActive(false);
    }
}
