using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIRaycast : MonoBehaviour
{
    [SerializeField] private GraphicRaycaster graphicRaycaster;
    [SerializeField] private EventSystem eventSystem;
    private PointerEventData pointerEvent;
    // Start is called before the first frame update
    private void Start()
    {
        graphicRaycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
    }

    public bool DoUIRaycast()
    {
        pointerEvent = new PointerEventData(eventSystem) {position = Input.mousePosition};

        var results = new List<RaycastResult>();

        graphicRaycaster.Raycast(pointerEvent, results);
        return results.Count == 0;
    }
}
