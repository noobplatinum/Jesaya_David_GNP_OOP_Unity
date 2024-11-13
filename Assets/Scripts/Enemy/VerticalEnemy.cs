using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalEnemy : BaseEnemy
{
    public float moveSpeed = 5f; 
    private float screenBound;

    void Start()
    {
        screenBound = Mathf.Abs(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1.1f, 0)).y);
        moveSpeed = -moveSpeed; 
    }

    void Update()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);

        if (Mathf.Abs(transform.position.y) > screenBound)
        {
            RespawnEnemy();
        }
    }

    void RespawnEnemy()
    {
        transform.position = new Vector3(Random.Range(-8f, 8f), screenBound, 0f);
    }
}
