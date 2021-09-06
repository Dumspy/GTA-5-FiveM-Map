using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ErrorHandler : MonoBehaviour
{
    [SerializeField] private GameObject errorTextObject;
    [SerializeField] private TMP_Text errorText;
    public void SetError(string text)
    {
        errorText.text = "Error: " + text;
        errorTextObject.SetActive(true);
    }
}
