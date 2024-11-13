using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : BaseEnemy
{
    public float enemySpeed = 5f; 
    private bool movDirection = true; // Arah pergerakan Enemy (true = kanan, false = kiri)
    private float screenBound;

    [SerializeField] private Weapon bossWeapon;

    void Start()
    {
        // Menghitung batas layar di kanan dan kiri dari tengah layar
        screenBound = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 0)).x; // Batas kanan/kiri (jarak dari tengah layar)
        SpawnAtRandomSide();
        StartCoroutine(FireWeaponContinuously());
    }

    void Update()
    {
        if (movDirection)
        {
            transform.Translate(Vector2.right * enemySpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * enemySpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x) > screenBound)
        {
            movDirection = !movDirection;
            RandomizeYPosition(); 
        }
    }

    void SpawnAtRandomSide()
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

    void RandomizeYPosition()
    {
        float newY = Random.Range(-4f, 4f); 
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private IEnumerator FireWeaponContinuously()
    {
        while (true)
        {
            bossWeapon.Fire();
            yield return new WaitForSeconds(bossWeapon.shootIntervalInSeconds); // Adjust the firing rate as needed
        }
    }

}