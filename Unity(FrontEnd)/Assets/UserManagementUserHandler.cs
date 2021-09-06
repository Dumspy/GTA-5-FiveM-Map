using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class UserManagementUserHandler : MonoBehaviour
{
    public Agent myData;
    private string apiKey;
    [SerializeField] private TMP_Text usernameText;
    [SerializeField] private TMP_Text authText;
    [SerializeField] private RankTextHandler rankText;
    [SerializeField] private TMP_InputField accessLevelText;
    [SerializeField] private TMP_Text sessionText;

    public void Setup(Agent user, string key)
    {
        myData = user;
        usernameText.text = user.username;
        authText.text = user.authed == "true" ? "True" : "False";
        rankText.SetRank(user.rank);
        accessLevelText.text = "" + user.accessLevel;
        StartCoroutine(UpdateSessionText(key, user._id));
        apiKey = key;
    }

    public void StartSaveCoroutine()
    {
        StartCoroutine(SaveUser());
    }

    private IEnumerator SaveUser()
    {
        var form = new WWWForm();
        form.AddField("id",myData._id);
        form.AddField("auth",myData.authed);
        form.AddField("rank",myData.rank);
        form.AddField("accessLevel",myData.accessLevel);
        using (var webRequest = UnityWebRequest.Post("https://dispyapi.auxera.net/api/map/updateUser&key="+ apiKey,form)) {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
            }
            else
            {
            }
        }       
    }

    private IEnumerator UpdateSessionText(string key, string userId)
    {
        using (var webRequest =
            UnityWebRequest.Get("https://dispyapi.auxera.net/api/map/getActiveSessionById&id=" + userId + "&key=" +
                                key))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
            }
            else
            {
                var currentState = webRequest.downloadHandler.text;

                switch (currentState)
                {
                    case "true":
                        sessionText.text = "Active";
                        break;
                    case "false":
                        sessionText.text = "Inactive";
                        break;
                }
            }
        }
    }

    public void UpdateAccessLevel()
    {
        if (accessLevelText.text == "")
        {
            return;
        }

        myData.accessLevel = Convert.ToInt32(accessLevelText.text);
    }

    public void UpdateAuth()
    {
        if (myData.authed == "false")
        {
            authText.text = "True";
            myData.authed = "true";
            return;
        }

        authText.text = "False";
        myData.authed = "false";
    }
}