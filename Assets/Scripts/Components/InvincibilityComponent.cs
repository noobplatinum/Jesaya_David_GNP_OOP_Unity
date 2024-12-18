using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HitboxComponent))] // Wajibkan hitbox dan invincibility component
[RequireComponent(typeof(SpriteRenderer))]

public class InvincibilityComponent: MonoBehaviour
{
    #region Datamembers

    #region Editor Settings
    [SerializeField] private int blinkingCount = 7; // Jumlah blink
    [SerializeField] private float blinkInterval = 0.1f; 
    [SerializeField] private Material blinkMaterial; // Gunakan material ini saat blink
    #endregion

    #region Private Fields
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    private Coroutine blinkRoutine;
    public bool isInvincible = false;

    #endregion
    #endregion

    #region Methods
    #region Unity Callbacks
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Ambil spriterenderer dan cari materialnya
        if (spriteRenderer != null)
        {
            originalMaterial = spriteRenderer.material;
        }
    }
    #endregion
    
    private IEnumerator BlinkingRoutine()
    {
        isInvincible = true; // Saat invincible, maka mulai blink dengan interval

        for (int i = 0; i < blinkingCount; i++)
        {
            spriteRenderer.material = blinkMaterial;
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.material = originalMaterial;
            yield return new WaitForSeconds(blinkInterval);
        }

        isInvincible = false; // Matikan invincibility
        blinkRoutine = null;
    }

    public void StartBlinking() // Coroutine
    {
        if (isInvincible == false)
        {
            if (blinkRoutine != null)
            {
                StopCoroutine(blinkRoutine);
            }
            blinkRoutine = StartCoroutine(BlinkingRoutine());
        }
    }
    #endregion
}