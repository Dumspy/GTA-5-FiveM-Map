using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankTextHandler : MonoBehaviour
{
    [SerializeField] private UserManagementUserHandler userHandler;
    [SerializeField] private TMP_InputField rankField;

    public void Respond()
    {
        switch (rankField.text)
        {
            case "0":
                userHandler.myData.rank = 0;
                rankField.text = "Kadet";
                break;
            case "1":
                userHandler.myData.rank = 1;
                rankField.text = "Agent";
                break;
            case "2":
                userHandler.myData.rank = 2;
                rankField.text = "Ledelse";
                break;
            case "3":
                userHandler.myData.rank = 3;
                rankField.text = "Rigspolitiet";
                break;
            default:
                userHandler.myData.rank = 0;
                rankField.text = "Kadet";
                break;
        }
    }

    public void SetRank(int rank)
    {
        switch (rank)
        {
            case 0:
                userHandler.myData.rank = 0;
                rankField.text = "Kadet";
                break;
            case 1:
                userHandler.myData.rank = 1;
                rankField.text = "Agent";
                break;
            case 2:
                userHandler.myData.rank = 2;
                rankField.text = "Ledelse";
                break;
            case 3:
                userHandler.myData.rank = 3;
                rankField.text = "Rigspolitiet";
                break;
            default:
                userHandler.myData.rank = 0;
                rankField.text = "Kadet";
                break;
        }
    }
}