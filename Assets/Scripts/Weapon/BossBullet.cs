using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
public class BossBullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20f; 
    private Rigidbody2D rb;      
    [SerializeField] private float timeoutDelay = 1f;

    public IObjectPool<BossBullet> objectPool;

    public IObjectPool<BossBullet> ObjectPool { set => objectPool = value; }

    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(timeoutDelay));
    }

    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        Rigidbody2D rBody = GetComponent<Rigidbody2D>();
        rBody.velocity = new Vector2(0, 0);
        rBody.angularVelocity = 0;
        objectPool.Release(this);
    }

    public void SetObjectPool(IObjectPool<BossBullet> objectPool)
    {
        this.objectPool = objectPool;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if((other.CompareTag("BossEnemy")) == false)
        {
            Debug.Log("Bullet triggered by: " + other.tag);
            objectPool.Release(this);
        }
    }
}