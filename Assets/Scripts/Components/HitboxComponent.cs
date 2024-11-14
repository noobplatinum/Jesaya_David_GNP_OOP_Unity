
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    [SerializeField] private HealthComponent health;
    private InvincibilityComponent flashComponent;
    private void Start()
    {
        flashComponent = GetComponent<InvincibilityComponent>();
    }

    public void Damage(int damage)
    {
        if (health != null && (flashComponent == null || !flashComponent.isInvincible))
        {
            health.Subtract(damage);
        }
    }

    public void Damage(Bullet bullet)
    {
        if (health != null && (flashComponent == null || !flashComponent.isInvincible))
        {
            health.Subtract(bullet.damage);
        }
    }

    public void Damage(BossBullet bossBullet)
    {
        if (health != null && (flashComponent == null || !flashComponent.isInvincible))
        {
            health.Subtract(bossBullet.damage);
        }
    }
}
