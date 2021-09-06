using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ReportList
{
    public Report[] Values;
}

[System.Serializable]
public class Creator
{
    [SerializeField] public string _id;
    [SerializeField] public string username;
}

[System.Serializable]
public class Report
{
    [SerializeField] public string _id;
    [SerializeField] public string title;
    [SerializeField] public string timeOfEvents;
    [SerializeField] public string timeOfReport;
    [SerializeField] public string location;
    [SerializeField] public string[] agentsInvolved;
    [SerializeField] public string[] poisInvolved;
    [SerializeField] public string[] gangsInvolved;
    [SerializeField] public string report;
    [SerializeField] public Creator creator;
    [SerializeField] public int accessLevel;
}