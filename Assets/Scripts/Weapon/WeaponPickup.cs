using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour // Tujuan : Pickup Object
{
    [SerializeField] private Weapon weaponHolder; // Weapon yang dipegang
    private Weapon weapon;
    private static Weapon activeWeapon; 

    void Awake() // Instantiasi Weapon
    {
        weapon = Instantiate(weaponHolder);
    }

    void Start() 
    {
        if(weapon != null) // Start dengan mematikan semua weapon
        {
            weapon.gameObject.SetActive(false);
            TurnVisual(false, weapon);
        }
    }

    void OnTriggerEnter2D(Collider2D other) // Cek collision -> tempel weapon
    {

        if(other.CompareTag("Player"))
        {
            Transform shipTransform = other.transform.Find("Ship");
            if(shipTransform != null)
            {
                if(weapon != null)
                {
                    AttachWeaponToPlayer(shipTransform); // Tempel dan aktifkan
                    ActivateWeapon(weapon);
                    Player.Instance.weaponName = weapon.name;
                }
            }
        }
    }

    void ActivateWeapon(Weapon newWeapon)
    {
        if (activeWeapon != null) // Matikan weapon sekarang
        {
            activeWeapon.gameObject.SetActive(false);
            TurnVisual(false, activeWeapon);
        }

        activeWeapon = newWeapon; // Aktifkan weapon baru
        activeWeapon.gameObject.SetActive(true);
        TurnVisual(true, activeWeapon);
    }

    void TurnVisual(bool on)
    {
        foreach (var component in weapon.GetComponentsInChildren<MonoBehaviour>())
        {
            component.enabled = on;
        }
    }

    void TurnVisual(bool on, Weapon weapon) 
    {
        foreach (var component in weapon.GetComponentsInChildren<MonoBehaviour>())
        {
            component.enabled = on;
        }
    }

    void AttachWeaponToPlayer(Transform playerTransform) // Transformasi
    {
        weapon.transform.SetParent(playerTransform); // "Menempel". Jadi, weapon bergerak bersama player
        weapon.transform.localPosition = new Vector3(0, 0, 0); 
        weapon.transform.localRotation = Quaternion.identity; 


        SpriteRenderer weaponRenderer = weapon.GetComponent<SpriteRenderer>();
        if (weaponRenderer != null)
        {
            weaponRenderer.sortingLayerName = "Player"; 
            weaponRenderer.sortingOrder = -1; 
        } // Prioritas lebih rendah dari player agar tidak mengambang
    }

    public static Weapon GetActiveWeapon()
    {
        return activeWeapon;
    }
}