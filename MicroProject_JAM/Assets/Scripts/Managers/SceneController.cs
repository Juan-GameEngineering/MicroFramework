using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private Animator anim;

    bool isLoading;

    private void Start()
    {
        instance = this;
        isLoading = false;
        anim = GetComponent<Animator>();
    }

    public void NextLevel(int index)
    {
        if (!isLoading)
            StartCoroutine(LoadLevel(index));
    }

    IEnumerator LoadLevel(int index)
    {
        isLoading = true;
        anim.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        //AudioManager.instance.PlaySFX(null);
        AudioManager.instance.PlaySong(null, false);
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        while (!operation.isDone)
        {
            yield return null;
        }

        anim.SetTrigger("Start");

        isLoading = false;
    }
}
