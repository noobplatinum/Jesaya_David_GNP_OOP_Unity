using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Animator transAnim;

    void Awake() // Baca animator
    {
        if (transAnim != null)
        {
            Debug.Log("Loading animator transisi sukses!");
        }
        else
        {
            Debug.LogWarning("Animator tidak ditemukan.");
        }
    }

    IEnumerator LoadSceneAsync(string sceneName) // Saat ingin load scene, trigger end & start, lalu posisikan player dan load scene
    {
        transAnim.SetTrigger("End");

        yield return new WaitForSeconds(1);

        SceneManager.LoadSceneAsync(sceneName);

        Player.Instance.transform.position = new Vector3(0, -4.5f);

        transAnim.SetTrigger("Start");
    }

    public void LoadScene(string sceneName) // Co-routine : Kode dalam multiple frame (animasi)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}