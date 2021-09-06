using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputDataEditMenu : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_InputField titleTextField;
    [SerializeField] private TMP_InputField creatorTextField;
    [SerializeField] private TMP_InputField timeTextField;
    [SerializeField] private TMP_InputField descriptionTextField;
    [SerializeField] private GameObject actuallyTheEditMenu;
    private Waypoint currentOpen;

    public void Setup(Waypoint data)
    {
        currentOpen = data;
        iconImage.sprite = gameManager.icons[data.iconId];
        iconImage.color = new Color(data.color.r / 255.0f, data.color.g / 255.0f, data.color.b / 255.0f);
        titleTextField.text = data.name;
        descriptionTextField.text = data.description;
    }

    public void OpenEditMenu()
    {
        actuallyTheEditMenu.GetComponent<WaypointUpdater>().Setup(currentOpen);
        actuallyTheEditMenu.SetActive(true);
        actuallyTheEditMenu.transform.localPosition = transform.localPosition;
        gameObject.SetActive(false);
    }
}