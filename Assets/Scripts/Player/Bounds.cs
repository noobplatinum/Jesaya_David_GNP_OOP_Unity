using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    private Vector2 screenB;
    private float objectWidth;
    private float objectHeight;
    private Transform shipTransform;

    void Start()
    {
        screenB = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)); //Baca dimensi screen
        
        shipTransform = transform.Find("Ship"); //Ambil ship dan baca dimensinya
        if (shipTransform != null)
        {
            SpriteRenderer shipRenderer = shipTransform.GetComponent<SpriteRenderer>();
            if (shipRenderer != null)
            {
                objectWidth = shipRenderer.bounds.size.x / 2;
                objectHeight = shipRenderer.bounds.size.y / 2; // Agar tetap terlihat di screen
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenB.x * -1 + objectWidth + 0.3f, screenB.x - objectWidth - 0.3f);
        viewPos.y = Mathf.Clamp(viewPos.y, screenB.y * -1 + objectHeight, screenB.y - objectHeight - 0.3f); // Clamp ke batas-batas layar tadi
        transform.position = viewPos;
    }
}