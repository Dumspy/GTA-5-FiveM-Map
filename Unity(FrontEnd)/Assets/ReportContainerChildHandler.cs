using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReportContainerChildHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text dataText;
    private ReportController reportController;
    
    public Report myData;

    private void Start()
    {
        reportController = FindObjectOfType<ReportController>();
    }

    private static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
    {
        var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
        dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp);
        return dtDateTime;
    }

    public void Setup(Report report)
    {
        myData = report;
        dataText.text = $"{myData.title}  |  {UnixTimeStampToDateTime(Convert.ToInt64(myData.timeOfReport))}  |  {myData.creator.username}";
    }

    public void ViewReport()
    {
        reportController.ViewReport(myData);
    }
}
