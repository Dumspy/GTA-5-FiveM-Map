using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UserManagementHandler : MonoBehaviour
{
    [SerializeField]private DataContainer dataContainer;
    [SerializeField]private AgentList userList;
    [SerializeField]private List<GameObject> spawnedObjects = new List<GameObject>();
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject userObject;
    private void Start()
    {
        dataContainer = FindObjectOfType<DataContainer>();
    }

    public void StartUp()
    {
        StartCoroutine(GetUsers());
    }


    private IEnumerator GetUsers()
    {
        yield return new WaitForSeconds(0.2f);
        using (var webRequest = UnityWebRequest.Get("https://dispyapi.auxera.net/api/map/getAllUser&key="+dataContainer.apiKey)){
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                var json = webRequest.downloadHandler.text;
                userList = JsonUtility.FromJson<AgentList>(json);
                SpawnUserObjects();
            }
        }
    }

    private void SpawnUserObjects()
    {
        foreach (var curObject in spawnedObjects)
        {
            Destroy(curObject);
        }
        spawnedObjects.Clear();
        foreach (var user in userList.Values)
        {
            var tempObject = Instantiate(userObject, content.transform);
            spawnedObjects.Add(tempObject);
            tempObject.GetComponent<UserManagementUserHandler>().Setup(user,dataContainer.apiKey);
        }
    }
}