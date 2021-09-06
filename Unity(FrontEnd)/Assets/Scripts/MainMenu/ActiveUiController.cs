using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveUiController : MonoBehaviour
{
    [SerializeField] private GameObject accountUiErrorText;
    [SerializeField] private GameObject loginUi;
    [SerializeField] private GameObject createAccountUi;
    [SerializeField] private GameObject mainMenuUi;
    [SerializeField] private GameObject userManagementUi;

    private void Start()
    {
        if (FindObjectOfType<DataContainer>().apiKey != "")
        {
            ChangeScreen(2);
            return;
        }
        ChangeScreen(0);
    }

    private enum Screens
    {
        LoginUi,
        CreateAccountUi,
        MainMenu,
        UserManagementUi
    }

    private Screens currentScreen = Screens.LoginUi;

    private void DisableCurrentUi()
    {
        switch (currentScreen)
        {
            case Screens.LoginUi:
                loginUi.SetActive(false);
                break;
            case Screens.CreateAccountUi:
                createAccountUi.SetActive(false);
                break;
            case Screens.MainMenu:
                mainMenuUi.SetActive(false);
                break;
            case Screens.UserManagementUi:
                userManagementUi.SetActive(false);
                break;
        }
    }

    private void ChangeScreen(int id, bool disableCurrent = true)
    {
        if (disableCurrent)
        {
            DisableCurrentUi();
            accountUiErrorText.SetActive(false);
        }

        switch (id)
        {
            case 0:
                loginUi.SetActive(true);
                break;
            case 1:
                createAccountUi.SetActive(true);
                break;
            case 2:
                mainMenuUi.SetActive(true);
                break;
            case 3:
                userManagementUi.SetActive(true);
                userManagementUi.GetComponent<UserManagementHandler>().StartUp();
                break;
        }

        currentScreen = (Screens) id;
    }

    public void ChangeUi(int id)
    {
        ChangeScreen(id);
    }

    public void LoadMapScene()
    {
        SceneManager.LoadScene(1);
    }
}