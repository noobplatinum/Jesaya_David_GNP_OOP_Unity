using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private Animator sceneTransitionAnimator;

    void Awake()
    {
        // Find the Animator component in the "SceneTransition" object
        sceneTransitionAnimator = GetComponentInChildren<Animator>();

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
        // Set the "End" trigger to start the transition animation
        if (sceneTransitionAnimator != null)
        {
            sceneTransitionAnimator.SetTrigger("Start");
        }

        yield return new WaitForSeconds(1);

        // Load the new scene asynchronously
        SceneManager.LoadSceneAsync(sceneName);

        // Set the player's position
        Player.Instance.transform.position = new Vector3(0, -4.5f);

        // Set the "Start" trigger to end the transition animation
        if (sceneTransitionAnimator != null)
        {
            sceneTransitionAnimator.SetTrigger("End");
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}