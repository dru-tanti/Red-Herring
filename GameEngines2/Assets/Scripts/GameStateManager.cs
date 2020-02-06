using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private bool isPaused;

    private string _pauseMenuScene = "PauseMenu";

    private void Start()
    {
        SceneManager.sceneLoaded += OnPauseMenuLoaded;
        SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            
            if (isPaused)
            {
                ActivateMenu();
            }
            else 
            {
                DeactivateMenu();
            }
        }    
    }
      void ActivateMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        _pauseMenu.gameObject.SetActive(true);
    }

     void DeactivateMenu()
    {
        Time.timeScale = 1;
        _pauseMenu.gameObject.SetActive(false);
    }

    private void OnPauseMenuLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == _pauseMenuScene)
        {
            // sets up the OptionsMenu script so it can complement this one.
            _pauseMenu = GameObject.FindObjectOfType<PauseMenu>();

            // onBackButton is a blank function similar to sceneLoaded
            _pauseMenu.onResume += DeactivateMenu;
            _pauseMenu.gameObject.SetActive(false);

            // this makes sure that there is no garbage left in sceneLoaded
            // once this script is destroyed.
            SceneManager.sceneLoaded -= OnPauseMenuLoaded;
        }
    }
}
