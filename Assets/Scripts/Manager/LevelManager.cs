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
        // Find the Animator component in the "SceneTransition" object
        if (sceneTransitionAnimator != null)
        {
            Debug.Log("SceneTransition Animator found.");
        }
        else
        {
            Debug.LogWarning("SceneTransition Animator not found.");
        }

        // Do something on Awake, e.g., make an object appearance false
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