using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    [SerializeField] private Bullet bullet;

    [SerializeField] private int damage; // Damage dari bullet, atau damage dari collision

    private void OnTriggerEnter2D(Collider2D collision) // Ketika terjadi collision dengan collider lain
    {
        // Jika tag sama, maka tidak akan terjadi collision        
        if (collision.CompareTag(gameObject.tag))
        {
            return;
        }

        HitboxComponent hitbox = collision.GetComponent<HitboxComponent>();
        // Kalau beda tag, ambil hitbox
        if (hitbox != null)
        {
            Debug.Log("Collided with " + collision.name); // Jika ada bullet, maka damage dari bullet
            if (bullet != null)
            {
                hitbox.Damage(bullet);
            } 
            else // Jika tanpa bullet, akan kena damage tabrakan
            {
                hitbox.Damage(damage);
            }
            InvincibilityComponent blinkComponent = collision.GetComponent<InvincibilityComponent>(); // Mulai blink saat terkena damage
            if (blinkComponent != null && blinkComponent.isInvincible == false)
            {
                blinkComponent.StartBlinking();
            }
        }
    }
}