using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalEnemy : BaseEnemy
{
    public float moveSpeed = 5f; 
    private float screenBound;

    void Start() // Ambil batas kamera dan spawn dengan arah ke bawah
    {
        screenBound = Mathf.Abs(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1.1f, 0)).y);
        moveSpeed = -moveSpeed; // (Jika tidak, akan spawn ke atas)
        RandomSpawn(); 
    }

    void Update()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime); // Gerak ke bawah

        if (Mathf.Abs(transform.position.y) > screenBound) // Jika melewati batas layar, spawn ulang
        {
            RandomSpawn();
        }
    }

    void RandomSpawn()
    {
        transform.position = new Vector3(Random.Range(-8f, 8f), screenBound, 0f); // Posisi X random
    }
}
