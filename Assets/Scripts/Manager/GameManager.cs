using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public LevelManager LevelManager { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LevelManager = FindObjectOfType<LevelManager>();
        
        //HideAllExceptCameraAndPlayer();
    }

    private void HideAllExceptCameraAndPlayer()
    {
        foreach (GameObject obj in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (obj.CompareTag("MainCamera") || obj.CompareTag("Player"))
            {
                continue;
            }
            obj.SetActive(false);
        }
    } 
}