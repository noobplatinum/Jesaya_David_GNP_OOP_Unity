using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float changeDirectionInterval = 2f;
    private Vector2 newPosition;
    private float changeDirectionTimer;

    void Start()
    {
        ChangePosition();
        changeDirectionTimer = changeDirectionInterval;
    }

    void Update()
    {
        transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);

        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

        changeDirectionTimer -= Time.deltaTime;
        if (changeDirectionTimer <= 0)
        {
            ChangePosition();
            changeDirectionTimer = changeDirectionInterval;
        }

        if (WeaponPickup.GetActiveWeapon() == null)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
        }
    }

    private void ChangePosition()
    {
        newPosition = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LevelManager.LoadScene("Main");
        }
    }
}