using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class AccountCreater : MonoBehaviour
{
    [SerializeField] private ErrorHandler errorHandler;
    [SerializeField] private ActiveUiController activeUiController;
    [SerializeField] private UsernameHandler usernameHandler;
    [SerializeField] private PasswordHandler passwordHandler;

    public void CreateAccount()
    {
        var username = usernameHandler.GetUsername();
        if(username == null){Debug.Log("Username problem");return;}

        var password = passwordHandler.GetPassword();
        if(password == null){Debug.Log("Password problem");return;}

        StartCoroutine(CreateAccountCoroutine(username,password));
    }

    private IEnumerator CreateAccountCoroutine(string username, string password)
    {
        var form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password",password);

        using (var www = UnityWebRequest.Post("https://dispyapi.auxera.net/api/map/createUser", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.downloadHandler.text == "accountCreated")
                {
                    usernameHandler.ResetField();
                    passwordHandler.ResetField();
                    activeUiController.ChangeUi(0);
                }
                else
                {
                    errorHandler.SetError(www.downloadHandler.text);
                }
            }
        }
    }
}