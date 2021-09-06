using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class SessionHandler : MonoBehaviour
{
    private string currentState = "null";
    private DataContainer dataContainer;
    [SerializeField]private TMP_Text buttonText;

    private void Start()
    {
        dataContainer = FindObjectOfType<DataContainer>();
        StartCoroutine(UpdateCurrentState());
    }

    private IEnumerator UpdateCurrentState(bool retrySession = false)
    {
        yield return new WaitForSeconds(0.2f);
        using (var webRequest = UnityWebRequest.Get("https://dispyapi.auxera.net/api/map/getActiveSession&key=" + dataContainer.apiKey)) {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();


            if (webRequest.isNetworkError)
            {
            }
            else
            {
                currentState = webRequest.downloadHandler.text;
                if (retrySession)
                {
                    StartCoroutine(UpdateSessionCoroutine());
                }

                switch (currentState)
                {
                    case "true":
                        buttonText.text = "Tjek ud";
                        break;
                    case "false":
                        buttonText.text = "Tjek ind";
                        break;
                }
            }
        }
    }

    public void UpdateSession()
    {
        StartCoroutine(UpdateSessionCoroutine());
    }

    private IEnumerator UpdateSessionCoroutine()
    {
        var form = new WWWForm();
        switch (currentState)
        {
            case "true":
                using (var webRequest =
                    UnityWebRequest.Post("https://dispyapi.auxera.net/api/map/checkUd&key=" + dataContainer.apiKey,form))
                {
                    // Request and wait for the desired page.
                    yield return webRequest.SendWebRequest();


                    if (webRequest.isNetworkError)
                    {
                        Debug.Log(webRequest.error);
                    }
                    else
                    {
                        currentState = "false";
                        buttonText.text = "Tjek ind";
                    }
                }

                break;
            case "false":
                using (var webRequest = UnityWebRequest.Post("https://dispyapi.auxera.net/api/map/checkIn&key=" + dataContainer.apiKey, form)) {
                    // Request and wait for the desired page.
                    yield return webRequest.SendWebRequest();


                    if (webRequest.isNetworkError)
                    {
                        Debug.Log(webRequest.error);
                    }
                    else
                    {
                        currentState = "true";
                        buttonText.text = "Tjek ud";
                    }
                }

                break;
            default:
                StartCoroutine(UpdateCurrentState(true));
                break;
        }
    }
}