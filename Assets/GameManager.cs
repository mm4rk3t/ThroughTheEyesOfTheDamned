using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _startSceneTransition;
    [SerializeField] private GameObject _endSceneTransition;
    [SerializeField] private GameObject _deathScreen;
    private void Start()
    {
        StartCoroutine(ToggleActiveTransition(_startSceneTransition, true, SceneManager.GetActiveScene().name));
    }

    public IEnumerator ToggleActiveTransition(GameObject scene, bool value, string sceneToLoad)
    {
        scene.SetActive(value);
        yield return new WaitForSeconds(1f);
        if (SceneManager.GetActiveScene().name != sceneToLoad)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            scene.SetActive(!value);
        }
        
    }
    public void OnDeath(bool value)
    {
        _deathScreen.SetActive(value);
    }

    public void ChangeScene(string scene)
    {
        StartCoroutine(ToggleActiveTransition(_endSceneTransition,true, scene));
    }
}
