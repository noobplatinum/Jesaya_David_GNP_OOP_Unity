using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20f; 
    private Rigidbody2D rb; // Dibaca, tapi redundant     
    [SerializeField] private float timeoutDelay = 1f; // Delay sebelum kembali ke pool
    [SerializeField] public int damage = 20; // Damage yang diberikan

    public IObjectPool<Bullet> objectPool;

    public IObjectPool<Bullet> ObjectPool { set => objectPool = value; }

    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(timeoutDelay));
    }

    public int getDamage()
    {
        return damage;
    }

    public Bullet GetBullet()
    {
        return this;
    } // Getter-getter untuk attack

    IEnumerator DeactivateRoutine(float delay) // Setelah delay, nonaktifkan kecepatan dan kembali ke pool
    {
        yield return new WaitForSeconds(delay);
        Rigidbody2D rBody = GetComponent<Rigidbody2D>();
        rBody.velocity = new Vector2(0, 0);
        rBody.angularVelocity = 0;
        objectPool.Release(this);
    }

    public void SetObjectPool(IObjectPool<Bullet> objectPool)
    {
        this.objectPool = objectPool;
    } // Set object pool 

    private void OnTriggerEnter2D(Collider2D other)
    {
        objectPool.Release(this);
    } 
}