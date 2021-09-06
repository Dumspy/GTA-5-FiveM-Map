using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IconColorController : MonoBehaviour
{
    [SerializeField] private Image iconObject;
    [SerializeField] private TMP_InputField redFieldText;
    [SerializeField] private TMP_InputField greenFieldText;
    [SerializeField] private TMP_InputField blueFieldText;

    public void ResetColor()
    {
        redFieldText.text = "";
        greenFieldText.text = "";
        blueFieldText.text = "";
        SetColor(new Color(0,0,0));
    }
    public void UpdateR()
    {
        if (redFieldText.text == "") {redFieldText.text = "0";}
        var value1 = int.Parse(redFieldText.text);
        var value2 = Mathf.Clamp(value1, 0, 255);
        var value = value2/255f;
        var color = iconObject.color;
        SetColor(new Color(value,color.g,color.b));
    }

    public void UpdateG()
    {
        if (greenFieldText.text == "") {greenFieldText.text = "0";}
        var value1 = int.Parse(greenFieldText.text);
        var value2 = Mathf.Clamp(value1, 0, 255);
        var value = value2 / 255.0f;
        var color = iconObject.color;
        SetColor(new Color(color.r,value,color.b));
    }

    public void UpdateB()
    {
        if (blueFieldText.text == "") {blueFieldText.text = "0";}
        var value1 = int.Parse(blueFieldText.text);
        var value2 = Mathf.Clamp(value1, 0, 255);
        var value = value2 / 255.0f;
        var color = iconObject.color;
        SetColor(new Color(color.r,color.g,value));
    }

    public Color GetColor()
    {
        return iconObject.color;
    }

    public void SetColor(Color color)
    {
        iconObject.color = color;
    }
}
