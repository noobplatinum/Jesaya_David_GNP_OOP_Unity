using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float randomMovement = 2f;
    private Vector2 newPosition;
    private float randomizeTimer;

    void Start() // Start dalam arah yang random
    {
        ChangePosition();
        randomizeTimer = randomMovement;
    }

    void Update() 
    {
        transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime); // Putar

        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime); // Fungsi untuk movement mulus

        randomizeTimer -= Time.deltaTime; 
        if (randomizeTimer <= 0)
        {
            ChangePosition();
            randomizeTimer = randomMovement; // Saat randomTime sudah habis, acak gerakan
        }

        if (WeaponPickup.GetActiveWeapon() == null) // Hanya terlihat saat ada weapon
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

    private void ChangePosition() // Random vector
    {
        newPosition = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Transform uiTransform = GameManager.Instance.transform.Find("UI"); // Destroy UI scene ChooseWeapon
            if (uiTransform != null)
            {
                Destroy(uiTransform.Find("GalaxyText").gameObject);
                Destroy(uiTransform.Find("BlasterText").gameObject);
                Destroy(uiTransform.Find("InstructionText").gameObject);
            }
            GameManager.Instance.LevelManager.LoadScene("Main"); // Panggil definisi loadscene dari levelmanager
        }
    }
}