using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20f; 
    private Rigidbody2D rb;      
    [SerializeField] private float timeoutDelay = 1f;
    [SerializeField] public int damage = 20;

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
    }

    IEnumerator DeactivateRoutine(float delay)
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
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if((other.CompareTag("Player")) == false)
        {
            objectPool.Release(this);
        }
    }
}