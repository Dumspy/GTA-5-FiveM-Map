using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaypointList
{
    public Waypoint[] Values;
}

[System.Serializable]
public class Vec2
{
    public float x;
    public float y;
}

[System.Serializable]
public class Vec3
{
    public int r;
    public int g;
    public int b;
}

[System.Serializable]
public struct Waypoint
{
    [SerializeField] public string _id;
    [SerializeField] public string name;
    [SerializeField] public Vec2 pos;
    [SerializeField] public int iconId;
    [SerializeField] public string description;
    [SerializeField] public Vec3 color;
}