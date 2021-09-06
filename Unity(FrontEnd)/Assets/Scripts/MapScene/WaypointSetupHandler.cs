using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointSetupHandler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Waypoint myData;

    public void Setup(Waypoint wp)
    {
        myData = wp;
        gameObject.name = wp.name;
        transform.position = new Vector3(wp.pos.x, wp.pos.y);
        spriteRenderer.sprite = FindObjectOfType<GameManager>().icons[wp.iconId];
        spriteRenderer.color = new Color(wp.color.r / 255.0f, wp.color.g / 255.0f, wp.color.b / 255.0f);
    }

    public void WaypointUpdate(Waypoint wp)
    {
        myData.name = wp.name;
        gameObject.name = wp.name;

        spriteRenderer.sprite = FindObjectOfType<GameManager>().icons[wp.iconId];
        myData.iconId = wp.iconId;
        spriteRenderer.color = new Color(wp.color.r / 255.0f, wp.color.g / 255.0f, wp.color.b / 255.0f);
        myData.color.r = wp.color.r;
        myData.color.g = wp.color.g;
        myData.color.b = wp.color.b;

        myData.description = wp.description;
    }
}