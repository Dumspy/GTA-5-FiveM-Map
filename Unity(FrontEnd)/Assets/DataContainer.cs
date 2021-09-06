using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContainer : MonoBehaviour
{
    public string apiKey;
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}
