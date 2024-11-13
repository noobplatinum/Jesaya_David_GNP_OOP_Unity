using UnityEngine;
using System.Collections;

public class TargetingEnemy : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float startChaseDelay = 1f;
    private Transform playerTransform;
    private bool isChasing = false;
    private bool hasCollided = false;

    void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Start()
    {
        StartCoroutine(ChaseDelay());
    }

    void Update()
    {
        if (isChasing && !hasCollided && playerTransform != null)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        Vector3 offsetPosition = playerTransform.position + new Vector3(0, 0.6f, 0);
        Vector3 direction = (offsetPosition - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, offsetPosition) < 0.05f)
        {
            transform.position = offsetPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
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

    private IEnumerator ChaseDelay()
    {
        yield return new WaitForSeconds(startChaseDelay);
        isChasing = true;
    }
}