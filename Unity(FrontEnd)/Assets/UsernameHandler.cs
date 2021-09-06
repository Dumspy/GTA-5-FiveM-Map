using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class UsernameHandler : MonoBehaviour
{
    [SerializeField] private string usernameInUseEqual = "false";
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private ValidDataIconController validDataIconController;
    private string usernameInUse = "true";

    public void UserRespondStart()
    {
        if (usernameInputField.text == "")
        {
            usernameInUse = "true";
            UserRespond();
            return;
        }

        StartCoroutine(UpdateUsernameInUse(usernameInputField.text, 1));
    }

    private void UserRespond()
    {
        if (usernameInUse == usernameInUseEqual)
        {
            validDataIconController.ChangeState(1);
            return;
        }

        validDataIconController.ChangeState(2);
    }

    private IEnumerator UpdateUsernameInUse(string username, int id)
    {
        using (var webRequest = UnityWebRequest.Get("https://dispyapi.auxera.net/api/map/usernameInUse&username=" + username))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();


            if (webRequest.isNetworkError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                usernameInUse = webRequest.downloadHandler.text;
                switch (id)
                {
                    case 1:
                        UserRespond();
                        break;
                        ;
                    case 2:
                        break;
                }
            }
        }
    }

    public string GetUsername()
    {
        StartCoroutine(UpdateUsernameInUse(usernameInputField.text, 2));
        if (usernameInputField.text != "") return usernameInUse == usernameInUseEqual ? usernameInputField.text : null;
        return null;
    }
    
    public void ResetField()
    {
        validDataIconController.ChangeState(0);
        usernameInputField.text = "";
    }
}