using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        EventsHolder.OnLoadLevel += StartFadingToScene;
    }

    private void OnDisable()
    {
        EventsHolder.OnLoadLevel -= StartFadingToScene;
    }

    private void StartFadingToScene(string sceneName)
    {
        StartCoroutine(FadeTo(sceneName));
    }

    private IEnumerator FadeTo(string scene)
    {
        animator.SetTrigger("StartFade");

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadSceneAsync(scene);
    }
}