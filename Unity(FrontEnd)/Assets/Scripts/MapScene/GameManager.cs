using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum CameraState { CameraUnlocked, CameraLocked};
    public CameraState cameraState = CameraState.CameraUnlocked;
    
    public Sprite[] icons = new Sprite[0];
}
