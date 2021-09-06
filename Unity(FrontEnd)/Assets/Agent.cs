using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AgentList
{
    public Agent[] Values;
}

[System.Serializable]
public struct Agent
{
    [SerializeField] public string _id;
    [SerializeField] public string username;
    [SerializeField] public string authed;
    [SerializeField] public int rank;
    [SerializeField] public int accessLevel;
}