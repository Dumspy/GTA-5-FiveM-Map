using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ViewReportsHandler : MonoBehaviour
{
    [SerializeField] private GameObject child;
    [SerializeField] private GameObject container;
    private List<GameObject> spawnedObjects = new List<GameObject>();
    private DataContainer dataContainer;
    
    private ReportList reports;
    
    public void StartUp()
    {
        dataContainer = FindObjectOfType<DataContainer>();
        StartCoroutine(FetchReports());
    }

    private IEnumerator FetchReports()
    {
        yield return new WaitForSeconds(0.2f);
        using (var webRequest = UnityWebRequest.Get("https://dispyapi.auxera.net/api/map/getReports&key="+dataContainer.apiKey)){
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                var json = webRequest.downloadHandler.text;
                reports = JsonUtility.FromJson<ReportList>(json);
                SpawnChildren();
            }
        }
    }

    private void SpawnChildren()
    {
        foreach (var curObject in spawnedObjects)
        {
            Destroy(curObject);
        }
        
        foreach (var report in reports.Values)
        {
            var spawnedObject =Instantiate(child, container.transform);
            spawnedObject.GetComponent<ReportContainerChildHandler>().Setup(report);
            spawnedObjects.Add(spawnedObject);
        }
    }
}
