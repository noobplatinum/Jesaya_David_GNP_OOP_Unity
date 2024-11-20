using UnityEngine;
using System.Collections;

public class TargetingEnemy : BaseEnemy
{
    public float moveSpeed = 0.5f;
    public float startChaseDelay = 1f; // Jangan langsung kejar player setelah spawn
    private Transform playerTransform;
    private bool isChasing = false; // Status mengejar, penting agar ada delay
    private bool hasCollided = false;  // Setelah tabrakan, stop & destroy

    void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        } // Kalau ada player, ambil posisinya
    }

    void Start()
    {
        RandomSpawn();
        StartCoroutine(ChaseDelay()); // Mulai delay
    }

    void Update()
    {
        if (isChasing && !hasCollided && playerTransform != null)
        {
            FollowPlayer();
        } // Jika mengejar, dan belum tabrakan, dan player ada, maka kejar player
    }

    private void FollowPlayer() // Metode kejar player
    {
        Vector3 offsetPosition = playerTransform.position + new Vector3(0, 0.6f, 0); // Offset 0.6f agar enemy terclamp dengan benar (adjustment hitbox)
        Vector3 direction = (offsetPosition - transform.position).normalized; // Normalisasi arah
        transform.position += direction * moveSpeed * Time.deltaTime; // Kejar posisi tersebut

        if (Vector3.Distance(transform.position, offsetPosition) < 0.05f)
        {
            transform.position = offsetPosition;
        } // Kalau sudah dekat, snap
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Destroy
        {
            HandleCollision();
        }
    }

    private void HandleCollision()
    {
        hasCollided = true;
        gameObject.SetActive(false);
        Destroy(gameObject, 1f);
    }

    private IEnumerator ChaseDelay() // Setelah delay, mulai mengejar
    {
        yield return new WaitForSeconds(startChaseDelay);
        isChasing = true;
    }

    private void RandomSpawn() // Spawn di Y random
    {
        float newY = Random.Range(-4f, 4f); 
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}