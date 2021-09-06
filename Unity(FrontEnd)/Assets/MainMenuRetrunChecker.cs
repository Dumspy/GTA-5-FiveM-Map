using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuRetrunChecker : MonoBehaviour
{
    private bool firstButtonPressed;
    private double timeOfFirstButton;
    private bool reset;

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && firstButtonPressed)
        {
            if (Time.time - timeOfFirstButton < 0.5f)
                SceneManager.LoadScene(0);
            reset = true;
        }
 
        if(Input.GetKeyDown(KeyCode.Escape) && !firstButtonPressed) {
            firstButtonPressed = true;
            timeOfFirstButton = Time.time;
        }

        if (!reset) return;
        firstButtonPressed = false;
        reset = false;
    }
}
