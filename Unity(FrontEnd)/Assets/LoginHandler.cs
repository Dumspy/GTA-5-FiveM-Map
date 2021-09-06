using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoginHandler : MonoBehaviour
{
    [SerializeField] private ActiveUiController activeUiController;
    [SerializeField] private ErrorHandler errorHandler;
    [SerializeField] private UsernameHandler usernameHandler;
    [SerializeField] private PasswordHandler passwordHandler;

    public void LoginHandel()
    {
        var username = usernameHandler.GetUsername();
        if (username == null)
        {
            errorHandler.SetError("Username missing or wrong");
            return;
        }

        var password = passwordHandler.GetPassword();
        if (password == null)
        {
            errorHandler.SetError("Password too long or short");
            return;
        }

        StartCoroutine(LoginHandelCoroutine(username, password));
    }

    private IEnumerator LoginHandelCoroutine(string username, string password)
    {
        var form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        using (var www = UnityWebRequest.Post("https://dispyapi.auxera.net/api/map/loginUser", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.downloadHandler.text.Substring(0, 8) == "success:")
                {
                    FindObjectOfType<DataContainer>().apiKey = www.downloadHandler.text.Substring(8);
                    activeUiController.ChangeUi(2);
                }
                else
                {
                    errorHandler.SetError(www.downloadHandler.text);
                }
            }
        }
    }
}