using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    private Vector2 screenB;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        screenB = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    void FixedUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenB.x * -1 + objectWidth, screenB.x - objectWidth - 0.3f);
        viewPos.y = Mathf.Clamp(viewPos.y, screenB.y * -1 + objectHeight, screenB.y - objectHeight - 0.7f);
        transform.position = viewPos;
    }
}