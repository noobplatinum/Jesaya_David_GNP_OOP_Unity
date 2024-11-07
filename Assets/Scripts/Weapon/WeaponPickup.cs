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
    [SerializeField] private Weapon weaponHolder; 
    private Weapon weapon;
    private static Weapon activeWeapon; 

    void Awake() // Instantiasi Weapon
    {
        weapon = Instantiate(weaponHolder);
        Debug.Log("Weapon: " + weapon.name);
    }

    void Start() 
    {
        if(weapon != null)
        {
            weapon.gameObject.SetActive(false);
            TurnVisual(false, weapon);
            Debug.Log("Weapon: " + weapon.name + " is now inactive");
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("Collision: " + other.name + ", Tag: " + other.tag);

        if(other.CompareTag("Player"))
        {
            Debug.Log("Player Collision");
            Transform shipTransform = other.transform.Find("Ship");
            if(shipTransform != null)
            {
                if(weapon != null)
                {
                    AttachWeaponToPlayer(shipTransform);
                    ActivateWeapon(weapon);
                    Debug.Log("Weapon: " + weapon.name + " is now active");
                }
            }
        }
    }

    void ActivateWeapon(Weapon newWeapon)
    {
        if (activeWeapon != null)
        {
            activeWeapon.gameObject.SetActive(false);
            TurnVisual(false, activeWeapon);
        }

        activeWeapon = newWeapon;
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

    void AttachWeaponToPlayer(Transform playerTransform)
    {
        weapon.transform.SetParent(playerTransform);
        weapon.transform.localPosition = new Vector3(0, 0, 0); 
        weapon.transform.localRotation = Quaternion.identity; 


        SpriteRenderer weaponRenderer = weapon.GetComponent<SpriteRenderer>();
        if (weaponRenderer != null)
        {
            weaponRenderer.sortingLayerName = "Player"; 
            weaponRenderer.sortingOrder = -1; 
        }
    }

    public static Weapon GetActiveWeapon()
    {
        return activeWeapon;
    }
}