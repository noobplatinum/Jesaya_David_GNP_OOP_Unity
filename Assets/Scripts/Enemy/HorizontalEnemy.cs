using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalEnemy : BaseEnemy
{
    public float enemySpeed = 5f; 
    private bool movDirection = true; // Arah pergerakan Enemy (true = kanan, false = kiri)
    private float screenBound;

    void Start()
    {
        // Menghitung batas layar di kanan dan kiri dari tengah layar
        screenBound = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 0)).x; // Batas kanan/kiri (jarak dari tengah layar)
        SpawnAtRandomSide(); // Mulai dari sisi random
    }

    void Update()
    {
        if (movDirection) // Pergerakan bolak-balik
        {
            transform.Translate(Vector2.right * enemySpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * enemySpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate() // Ketika melewati batas layar, arah akan berubah dan Y random
    {
        if (Mathf.Abs(transform.position.x) > screenBound)
        {
            movDirection = !movDirection;
            RandomizeYPosition(); 
        }
    }

    void SpawnAtRandomSide() // Ambil nilai random, lalu transform posisi objek
    {
        float spawnY = Random.Range(-4f, 4f); 
        if (Random.value > 0.5f)
        {
            transform.position = new Vector3(-screenBound, spawnY, 0);
            movDirection = true; 
        }
        else
        {
            transform.position = new Vector3(screenBound, spawnY, 0);
            movDirection = false; 
        }
    }

    void RandomizeYPosition() // Metode random setelah melewati batas layar
    {
        float newY = Random.Range(-4f, 4f); 
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}