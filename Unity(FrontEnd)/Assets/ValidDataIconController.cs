using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValidDataIconController : MonoBehaviour
{
    [SerializeField]private Image dataIcon;
    [SerializeField]private List<Sprite> sprites = new List<Sprite>();
    private enum States{Hidden,Valid,Invalid}

    private States currentState = States.Hidden;

    public void ChangeState(int id)
    {
        currentState = (States) id;
        switch (currentState)
        {
            case States.Hidden:
                dataIcon.color = new Color(0,0,0,0);
                break;
            case States.Valid:
                dataIcon.sprite = sprites[0];
                dataIcon.color = new Color(134f/256,134f/256,134f/256);
                break;
            case States.Invalid:
                dataIcon.sprite = sprites[1];
                dataIcon.color = new Color(190f/256,15f/256,15f/256);
                break;
        }
    }
}
