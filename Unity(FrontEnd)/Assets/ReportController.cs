using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportController : MonoBehaviour
{
    [SerializeField] private ViewReportHandler viewReportHandler;
    public void ViewReport(Report reportToView)
    {
        viewReportHandler.Setup(reportToView);
    }
}
