using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Animator sceneTransitionAnimator;

    void Awake()
    {
        if (sceneTransitionAnimator != null)
        {
            Debug.Log("Loading animator transisi sukses!");
        }
        else
        {
            Debug.LogWarning("Animator tidak ditemukan.");
        }
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        sceneTransitionAnimator.SetTrigger("End");

        yield return new WaitForSeconds(1);

        SceneManager.LoadSceneAsync(sceneName);

        Player.Instance.transform.position = new Vector3(0, -4.5f);

        sceneTransitionAnimator.SetTrigger("Start");
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}