using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaypointUpdater : MonoBehaviour
{
    [SerializeField] private WebRequestManager webRequestManager;
    [SerializeField] private TMP_InputField titleInputField;

    [SerializeField] private TMP_InputField descriptionInputField;
    [SerializeField] private IconColorController iconColorController;
    [SerializeField] private ClickController clickController;
    [SerializeField] private IconController iconController;
    [SerializeField] private WaypointController waypointController;

    [SerializeField] private TMP_InputField redColorInputField;
    [SerializeField] private TMP_InputField greenColorInputField;
    [SerializeField] private TMP_InputField blueColorInputField;

    [SerializeField] private Waypoint currentWaypoint;

    public void Setup(Waypoint data)
    {
        currentWaypoint = data;
        titleInputField.text = data.name;
        iconController.UpdateIcon(data.iconId);
        var tempColor = new Color(data.color.r / 255.0f, data.color.g / 255.0f, data.color.b / 255.0f);
        iconColorController.SetColor(tempColor);
        descriptionInputField.text = data.description;

        redColorInputField.text = data.color.r + "";
        greenColorInputField.text = data.color.g + "";
        blueColorInputField.text = data.color.b + "";
    }

    public void UpdateWaypoint()
    {
        currentWaypoint.name = titleInputField.text;
        currentWaypoint.iconId = iconController.currentIcon;
        currentWaypoint.description = descriptionInputField.text;
        var tempColor = iconColorController.GetColor();
        currentWaypoint.color.r = System.Convert.ToInt32(tempColor.r * 255);
        currentWaypoint.color.g = System.Convert.ToInt32(tempColor.g * 255);
        currentWaypoint.color.b = System.Convert.ToInt32(tempColor.b * 255);

        waypointController.UpdateWaypoint(currentWaypoint);
        StartCoroutine(webRequestManager.UpdateWaypoint(currentWaypoint));
        clickController.EditClose();
    }

    public void DeleteWaypoint()
    {
        waypointController.DeleteWaypoint(currentWaypoint);
        StartCoroutine(webRequestManager.DeleteWaypoint(currentWaypoint._id));
        clickController.EditClose();
    }
}