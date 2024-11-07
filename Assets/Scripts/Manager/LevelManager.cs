using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    void Awake()
    {
        //do Something on Awake E.g. make an object appearence false
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadSceneAsync(sceneName);

        Player.Instance.transform.position = new(0, -4.5f);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}