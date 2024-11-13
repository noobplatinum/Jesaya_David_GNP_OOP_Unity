using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{  
    [Header("Weapon Stats")]
    [SerializeField] public float shootIntervalInSeconds = 1f;


    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] public Transform BulletSpawnPoint;

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;

    public readonly bool collectionCheck = false;
    public readonly int defaultCapacity = 30;
    public readonly int maxSize = 100;
    public float timer;
    public Transform parentTransform;
    private void Awake()
    {
    // Initialize the object pool
    objectPool = new ObjectPool<Bullet>(
        CreateProjectile,
        OnGetFromPool,
        OnReleaseToPool,
        OnDestroyPooledObject,
        collectionCheck,
        defaultCapacity,
        maxSize
    );
    }
    private Bullet CreateProjectile()
    {
        Bullet bulletInstance = Instantiate(bullet);
        bulletInstance.ObjectPool = objectPool;
        return bulletInstance;
    }

    private void OnGetFromPool(Bullet pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(Bullet pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(Bullet pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= timer && objectPool != null)
        {
            Bullet bulletObject = objectPool.Get();
            if(bulletObject == null)
            {
                return;
            }

            bulletObject.transform.SetPositionAndRotation(BulletSpawnPoint.position, BulletSpawnPoint.rotation);

            bulletObject.GetComponent<Rigidbody2D>().velocity = bulletObject.transform.up * bulletObject.bulletSpeed;

            bulletObject.Deactivate();

            timer = Time.time + shootIntervalInSeconds;
        }
    }

    // Make a function to fire constantly without input from the player (using intervals)
    public void Fire()
    {
        if (Time.time >= timer && objectPool != null)
        {
            Bullet bulletObject = objectPool.Get();
            if(bulletObject == null)
            {
                return;
            }

            bulletObject.transform.SetPositionAndRotation(BulletSpawnPoint.position, BulletSpawnPoint.rotation);

            bulletObject.GetComponent<Rigidbody2D>().velocity = -bulletObject.transform.up * bulletObject.bulletSpeed;

            bulletObject.Deactivate();

            timer = Time.time + shootIntervalInSeconds;
        }
    }
}

