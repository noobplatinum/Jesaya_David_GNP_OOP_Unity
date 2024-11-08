using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance
    {
        get;
        private set;
    }

    private PlayerMovement playerMovement;
    private Animator animator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;         
            DontDestroyOnLoad(gameObject);  
            transform.position = new Vector3(0, -4.5f, 0);
        }
        else
        {
            Destroy(gameObject);  
        }
    }

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        GameObject engineEffect = GameObject.Find("EngineEffect");
        if (engineEffect != null)
        {
            animator = engineEffect.GetComponent<Animator>();
        }
        else
        {
            Debug.LogWarning("EngineEffect tidak ditemukan.");
        }
    }

    private void FixedUpdate()
    {
        playerMovement.Move();
    }

    private void LateUpdate()
    {
        bool isMoving = playerMovement.IsMoving();
        animator.SetBool("IsMoving", isMoving);
    }

    public void SetWeapon(Weapon weapon)
    {
        if (weapon != null)
        {
            weapon.transform.SetParent(transform);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            weapon.gameObject.SetActive(true);
        }
    }
}
