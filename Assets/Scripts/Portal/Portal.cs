using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private float speed;           
    [SerializeField] private float rotateSpeed;       
    private Vector2 newPosition;                      

    void Start()
    {
        ChangePosition();
    }

    void Update()
    {
        transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);

        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

        float distance = Vector2.Distance(transform.position, newPosition);
        if (distance < 0.5f)
        {
            ChangePosition();
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