using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AccessLevelToShow : MonoBehaviour
{
    [SerializeField] private int accessLevelNeededToShow = 0;
    private DataContainer dataContainer;
    // Start is called before the first frame update
    private IEnumerator Start()
    {
        yield return dataContainer = FindObjectOfType<DataContainer>();
        using (var webRequest = UnityWebRequest.Get("https://dispyapi.auxera.net/api/map/getActiveSessionById&key=" + dataContainer.apiKey))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
            }
            else
            {
                var success = int.TryParse(webRequest.downloadHandler.text, out var apiRespond);
                if (!success) yield break;
                if (apiRespond >= accessLevelNeededToShow)
                {
                    gameObject.SetActive(true);
                }
            }
        }
    }

}
