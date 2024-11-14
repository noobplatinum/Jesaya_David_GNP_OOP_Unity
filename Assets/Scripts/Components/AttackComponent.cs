using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    [SerializeField] private Bullet bullet;

    [SerializeField] private BossBullet bossBullet;

    [SerializeField] private int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag(gameObject.tag))
        {
            return;
        }

        HitboxComponent hitbox = collision.GetComponent<HitboxComponent>();
        
        if (hitbox != null)
        {
            Debug.Log("Collided with " + collision.name);
            if (bullet != null)
            {
                hitbox.Damage(bullet);
            }
            else if (bossBullet != null)
            {
                hitbox.Damage(bossBullet);
            }
            else
            {
                hitbox.Damage(damage);
            }
            InvincibilityComponent flashComponent = collision.GetComponent<InvincibilityComponent>();
            if (flashComponent != null && flashComponent.isInvincible == false)
            {
                flashComponent.StartBlinking();
            }
        }
    }
}