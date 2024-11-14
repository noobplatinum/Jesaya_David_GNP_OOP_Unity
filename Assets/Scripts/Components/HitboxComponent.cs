
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    [SerializeField] private HealthComponent health;
    private InvincibilityComponent blinkComponent;
    private void Start()
    {
        blinkComponent = GetComponent<InvincibilityComponent>();
    }

    public void Damage(int damage)
    {
        if (health != null && (blinkComponent == null || !blinkComponent.isInvincible))
        {
            health.Subtract(damage);
        }
    }

    public void Damage(Bullet bullet)
    {
        if (health != null && (blinkComponent == null || !blinkComponent.isInvincible))
        {
            health.Subtract(bullet.damage);
        }
    }

    public void Damage(BossBullet bossBullet)
    {
        if (health != null && (blinkComponent == null || !blinkComponent.isInvincible))
        {
            health.Subtract(bossBullet.damage);
        }
    }
}
