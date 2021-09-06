using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaypointCreator : MonoBehaviour
{
    [SerializeField] private WebRequestManager webRequestManager;
    [SerializeField] private TMP_InputField titleInputField;

    [SerializeField] private TMP_InputField descriptionInputField;
    [SerializeField] private IconColorController iconColorController;
    [SerializeField] private ClickController clickController;
    [SerializeField] private IconController iconController;
    [SerializeField] private WaypointController waypointController;


    private void ResetMenu()
    {
        titleInputField.text = "";
        descriptionInputField.text = "";
        iconColorController.ResetColor();
        iconController.UpdateIcon(0);
    }

    public void CreateWaypoint()
    {
        StartCoroutine(CreateWaypointCoroutine());
    }

    private IEnumerator CreateWaypointCoroutine()
    {
        var tempColor = iconColorController.GetColor();
        var tempWp = new Waypoint
        {
            name = titleInputField.text,
            description = descriptionInputField.text,
            color = new Vec3
            {
                r = Convert.ToInt32(tempColor.r * 255),
                g = Convert.ToInt32(tempColor.g * 255),
                b = Convert.ToInt32(tempColor.b * 255)
            },
            pos = new Vec2
            {
                x = clickController.posClicked.x,
                y = clickController.posClicked.y
            },
            iconId = iconController.currentIcon
        };
        StartCoroutine(webRequestManager.PostWaypoint(tempWp));
        yield return new WaitForSeconds(0.3f);
        clickController.ContextClose();
        ResetMenu();
    }
}