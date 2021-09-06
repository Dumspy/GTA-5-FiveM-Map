using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class ViewReportHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField titleInputField;
    [SerializeField] private TMP_InputField agentsInvolvedInputField;
    [SerializeField] private TMP_InputField timeInputField;
    [SerializeField] private TMP_InputField poiGangInputField; 
    [SerializeField] private TMP_InputField accessLevelInputField;
    [SerializeField] private TMP_InputField locationInputField;
    [SerializeField] private TMP_InputField reportInputField;

    public void Setup(Report data)
    {
        titleInputField.text = data.title;
        var tempString = data.agentsInvolved.Aggregate("", (current, agentName) => current + (agentName + ","));
        tempString = tempString.Substring(tempString.Length - 1);
        agentsInvolvedInputField.text = tempString;
    }
}
