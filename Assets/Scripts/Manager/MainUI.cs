using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MainUI : MonoBehaviour
{
    public CombatManager combatManager;
    private Player player;
    public TextMeshProUGUI hpText;
    private TextMeshProUGUI ptsText;
    private TextMeshProUGUI waveText;
    public TextMeshProUGUI gameOverText;
    private TextMeshProUGUI waveStartText;
    private TextMeshProUGUI enemyCountText;
    private TextMeshProUGUI rosterText;
    private TextMeshProUGUI weaponText;
    private String weaponType;
    private bool deadFlag = false;
    void Start()
    {
        SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sortingLayerName = "Background";
            renderer.sortingOrder = -2;
        }

        combatManager = FindObjectOfType<CombatManager>();
        player = FindObjectOfType<Player>();
        hpText = GameObject.Find("HPCount").GetComponent<TextMeshProUGUI>();
        ptsText = GameObject.Find("PointCount").GetComponent<TextMeshProUGUI>();
        waveText = GameObject.Find("WaveCount").GetComponent<TextMeshProUGUI>();
        gameOverText = GameObject.Find("GameOverText").GetComponent<TextMeshProUGUI>();
        waveStartText = GameObject.Find("WaveStart").GetComponent<TextMeshProUGUI>();
        enemyCountText = GameObject.Find("EnemyCount").GetComponent<TextMeshProUGUI>();
        rosterText = GameObject.Find("RosterText").GetComponent<TextMeshProUGUI>();
        weaponText = GameObject.Find("WeaponText").GetComponent<TextMeshProUGUI>(); 

        switch(player.weaponName)
        {
            case "Weapon1(Clone)":
                weaponType = "Gatling X";
                break;
            case "Weapon2(Clone)":
                weaponType = "Rocket 6";
                break;
            case "Weapon3(Clone)":
                weaponType = "Zapgun Z";
                break;
            case "Weapon4(Clone)":
                weaponType = "Piston R";
                break;
            default:
                weaponType = "None";
                break;
        }

        weaponText.text = weaponType + " - Weapon Type";
        rosterText.text = "Roster - ";
        SetTextAlpha(gameOverText, 0);
        SetTextAlpha(waveStartText, 0);
    }

    private void Update()
    {
        ptsText.text = "Points - " + combatManager.totalPts;
        waveText.text = combatManager.waveNumber + " - Wave";

        if (player != null)
        {hpText.text = "HP - " + player.GetComponent<HealthComponent>().health;}
        else
        {
            hpText.text = "H/P";
            if(!deadFlag)
            {
                deadFlag = true;
                StartCoroutine(ShowGameOver());
            }
        }

        enemyCountText.text = combatManager.displayEnemies + " - Enemies"; 

        rosterText.text = combatManager.RosterStr;
    }

    private IEnumerator ShowGameOver()
    {
        float duration = 3f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / duration);
            SetTextAlpha(gameOverText, alpha);
            yield return null;
        }
    }

    public IEnumerator ShowWaveStart(int waveNumber)
    {
        int blinkCount = 4;
        float blinkDuration = 0.2f;

        for (int i = 0; i < blinkCount - 1; i++)
        {
            waveStartText.text = "Wave " + waveNumber + " ...";
            SetTextAlpha(waveStartText, 1f); 
            yield return new WaitForSeconds(blinkDuration);
            SetTextAlpha(waveStartText, 0f); 
            yield return new WaitForSeconds(blinkDuration);
        }

        waveStartText.text = "Begin!";
        SetTextAlpha(waveStartText, 1f);
        yield return new WaitForSeconds(blinkDuration * 1.5f);
        SetTextAlpha(waveStartText, 0f);
    }

    private void SetTextAlpha(TextMeshProUGUI text, float alpha)
    {
        Color color = text.color;
        color.a = alpha;
        text.color = color;
    }

}