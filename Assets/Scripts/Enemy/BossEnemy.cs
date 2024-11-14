using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BossEnemy : BaseEnemy
{
    public float enemySpeed = 2f; 
    private bool movDirection = true; // Arah pergerakan Enemy (true = kanan, false = kiri)
    private float screenBound;

    [Header("Weapon Stats")]
    [SerializeField] public float shootIntervalInSeconds = 1f;


    [Header("Bullets")]
    public BossBullet bullet;
    [SerializeField] public Transform BossBulletSpawnPoint;

    [Header("BossBullet Pool")]
    private IObjectPool<BossBullet> objectPool;

    public readonly bool collectionCheck = false;
    public readonly int defaultCapacity = 30;
    public readonly int maxSize = 100;
    public float timer;
    public Transform parentTransform;

    private void Awake()
    {
    // Initialize the object pool
        objectPool = new ObjectPool<BossBullet>(
            CreateProjectile,
            OnGetFromPool,
            OnReleaseToPool,
            OnDestroyPooledObject,
            collectionCheck,
            defaultCapacity,
            maxSize
        );
    }
    private BossBullet CreateProjectile()
    {
        BossBullet bulletInstance = Instantiate(bullet);
        bulletInstance.ObjectPool = objectPool;
        return bulletInstance;
    }

    private void OnGetFromPool(BossBullet pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(BossBullet pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(BossBullet pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }

    private void Update()
    {
        if (Time.time >= timer && objectPool != null)
        {
            BossBullet bulletObject = objectPool.Get();
            if(bulletObject == null)
            {
                return;
            }

            bulletObject.transform.SetPositionAndRotation(BossBulletSpawnPoint.position, BossBulletSpawnPoint.rotation);

            bulletObject.GetComponent<Rigidbody2D>().velocity = bulletObject.transform.up * bulletObject.bulletSpeed;

            bulletObject.Deactivate();

            timer = Time.time + shootIntervalInSeconds;
        }
    }

    void Start()
    {
        // Menghitung batas layar di kanan dan kiri dari tengah layar
        screenBound = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 0)).x; // Batas kanan/kiri (jarak dari tengah layar)
        SpawnAtRandomSide();
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x) > screenBound)
        {
            movDirection = !movDirection;
            RandomizeYPosition(); 
        }

        if (movDirection)
            {
                transform.Translate(Vector2.right * enemySpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.left * enemySpeed * Time.deltaTime);
            }
    }

    void SpawnAtRandomSide()
    {
        float spawnY = Random.Range(-2f, 3f); 
        if (Random.value > 0.5f)
        {
            transform.position = new Vector3(-screenBound, spawnY, 0);
            movDirection = true; 
        }
        else
        {
            transform.position = new Vector3(screenBound, spawnY, 0);
            movDirection = false; 
        }
    }

    void RandomizeYPosition()
    {
        float newY = Random.Range(-3f, 3f); 
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

}