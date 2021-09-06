using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;



public class GetActiveUsernames : MonoBehaviour
{
    private DataContainer dataContainer;
    private float cooldown = 28;
    [SerializeField]private TMP_Text tmpText;

    private static string UppercaseFirst(string s)
    {
        // Check for empty string.
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        // Return char and concat substring.
        return char.ToUpper(s[0]) + s.Substring(1);
    }
    
    private void Start()
    {
        dataContainer = FindObjectOfType<DataContainer>();
    }

    // Update is called once per frame
    private void Update()
    {
        cooldown += Time.deltaTime;

        if (!(cooldown > 30)) return;
        cooldown = 0;
        StartCoroutine(UpdateCoroutine());
    }

    private IEnumerator UpdateCoroutine()
    {
        using (var webRequest = UnityWebRequest.Get("https://dispyapi.auxera.net/api/map/getActiveSessions&key=" + dataContainer.apiKey))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
            }
            else
            {
                tmpText.text = "";
                var tempString = webRequest.downloadHandler.text;
                tempString = tempString.Substring(1, tempString.Length-2);
                var names = tempString.Split(',');
                foreach (var tempName in names)
                {
                    tmpText.text += UppercaseFirst(tempName)+Environment.NewLine;
                }
            }
        }
    }
}
