using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameManager GameManager;
    [SerializeField] private GameObject _startTransition;
    public void PlayGame()
    {
        StartCoroutine(ToggleActiveTransition(_startTransition, true));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    private void LoadScene()
    {
        SceneManager.LoadScene("Tutorial");
    }

    private IEnumerator ToggleActiveTransition(GameObject scene, bool value)
    {
        scene.SetActive(value);
        yield return new WaitForSeconds(1f);
        LoadScene();
    }

}
