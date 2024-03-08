using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBus;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string lostScreenSceneName;
    [SerializeField] string wonScreenSceneName;
    [SerializeField] string mainMenuSceneName;

    private void OnEnable()
    {
        EventBus<GameEnd>.OnEvent += EndGame;
    }

    private void OnDisable()
    {
        EventBus<GameEnd>.OnEvent -= EndGame;
    }

    private void EndGame(GameEnd pGameEnd)
    {
        if (pGameEnd.won)
        {
            SceneManager.LoadScene(wonScreenSceneName);
        } else
        {
            SceneManager.LoadScene(lostScreenSceneName);
        }
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
