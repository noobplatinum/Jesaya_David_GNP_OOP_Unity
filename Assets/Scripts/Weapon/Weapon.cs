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
    // Init pool
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
    private Bullet CreateProjectile() // Instansiasi bullet, masukkan object pool
    {
        Bullet bulletInstance = Instantiate(bullet);
        bulletInstance.ObjectPool = objectPool;
        return bulletInstance;
    }

    private void OnGetFromPool(Bullet pooledObject)
    {
        if(pooledObject != null)
        {pooledObject.gameObject.SetActive(true);}
    }

    private void OnReleaseToPool(Bullet pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(Bullet pooledObject)
    {
        Destroy(pooledObject.gameObject);
    } // Fungsi-fungsi handling event

    private void Update()
    {
        if (Time.time >= timer && objectPool != null)
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
        } // Hitung waktu, lalu get/aktifkan bullet dan gerakkan ke atas. Pakai posisi bulletspawnpoint dan nonaktifkan setelah delay
    }
}

