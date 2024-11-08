using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script Weapon.cs
public class Weapon : MonoBehaviour
    {
        public Transform parentTransform;
    }
*/

public class WeaponPickup : MonoBehaviour // Tujuan : Pickup Object
{
    [SerializeField] private Weapon weaponHolder; // Weapon yang dipegang
    private Weapon weapon;
    private static Weapon activeWeapon; 

    void Awake() // Instantiasi Weapon
    {
        weapon = Instantiate(weaponHolder);
        Debug.Log("Weapon: " + weapon.name);
    }

    void Start() 
    {
        if(weapon != null) // Start dengan mematikan semua weapon
        {
            weapon.gameObject.SetActive(false);
            TurnVisual(false, weapon);
            Debug.Log("Weapon: " + weapon.name + " tidak aktif");
        }
    }

    void OnTriggerEnter2D(Collider2D other) // Cek collision -> tempel weapon
    {
        Debug.Log("Collision: " + other.name + ", Tag: " + other.tag);

        if(other.CompareTag("Player"))
        {
            Debug.Log("Tabrakan dengan Player");
            Transform shipTransform = other.transform.Find("Ship");
            if(shipTransform != null)
            {
                if(weapon != null)
                {
                    AttachWeaponToPlayer(shipTransform); // Tempel dan aktifkan
                    ActivateWeapon(weapon);
                    Debug.Log("Weapon: " + weapon.name + " is now active");
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