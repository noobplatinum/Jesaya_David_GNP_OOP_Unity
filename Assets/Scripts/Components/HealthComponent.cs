using UnityEngine;
public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHealth; 
    private int health; // Current health

    public int getHealth()
    {
        return health;
    }

    private void Start()
    {
        health = maxHealth;
    }

    public void Subtract(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    } // Kalau 0, mati
}
