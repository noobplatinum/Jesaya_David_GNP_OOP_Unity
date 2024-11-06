using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponHolder;
    private Weapon weapon;

    void Awake() 
    {
        weapon = Instantiate(weaponHolder);
    }

    void Start() 
    {
        if(weapon != null)
        {
            weapon.gameObject.SetActive(false);
            TurnVisual(false, weapon);
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            Transform shipTransform = other.transform.Find("ship");
            if(shipTransform != null)
            {
                if(weapon != null)
                {
                    weapon.transform.parent = shipTransform;
                    weapon.gameObject.SetActive(true);
                    TurnVisual(true, weapon);
                }
            }
        }
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
}
