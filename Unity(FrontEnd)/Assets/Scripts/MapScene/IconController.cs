using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconController : MonoBehaviour
{
    public int currentIcon = 0;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject scrollView;
    [SerializeField] private Image imageToChange;

    public void UpdateIcon(int iconNumber)
    {
        currentIcon = iconNumber;
        imageToChange.sprite = gameManager.icons[Mathf.Clamp(iconNumber,0,gameManager.icons.Length-1)];
        
        scrollView.SetActive(false);
    }

    public void ToggleScrollView()
    {
        scrollView.SetActive(!scrollView.activeSelf);
    }
}
