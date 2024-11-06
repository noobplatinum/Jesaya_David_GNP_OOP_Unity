using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        if (animator == null)
        {
            Debug.LogError("Animator not assigned to LevelManager.");
        }
    }

    public IEnumerator LoadSceneAsync(string sceneName)
    {
        animator.SetTrigger("StartTransition");

        yield return new WaitForSeconds(1f); 

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        animator.SetTrigger("EndTransition");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}