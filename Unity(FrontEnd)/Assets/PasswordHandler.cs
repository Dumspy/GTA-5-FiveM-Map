using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PasswordHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private ValidDataIconController validDataIconController;
    private int passwordLength = 0;

    public void UserRespond()
    {
        passwordLength = passwordInputField.text.Length;
        if (passwordLength < 6 || passwordLength > 64)
        {
            validDataIconController.ChangeState(2);
            return;
        }

        validDataIconController.ChangeState(1);
    }

    public string GetPassword()
    {
        if (passwordLength < 6 || passwordLength > 64)
        {
            return null;
        }

        return passwordInputField.text;
    }

    public void ResetField()
    {
        validDataIconController.ChangeState(0);
        passwordInputField.text = "";
    }
}