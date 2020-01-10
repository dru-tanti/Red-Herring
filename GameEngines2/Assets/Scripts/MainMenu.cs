using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private string _optionsMenuSceneName = "OptionsMenu";

    private OptionsMenu _optionsMenu;

    void Start()
    {
        // SceneLoaded is a blank function that can be extended through any script.
        // here, we're checking the scene that is loaded (options menu) and preparing for use.
        SceneManager.sceneLoaded += OnOptionsMenuLoaded;

        // load the new scene as a part of the current one.
        SceneManager.LoadScene(_optionsMenuSceneName, LoadSceneMode.Additive);
    }

    // Opens the options menu.
    public void OpenOptionsMenu()
    {
        _optionsMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    // A function aimed at closing the options menu (once it has been loaded on Start)
    public void CloseOptionsMenu()
    {
        _optionsMenu.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame ()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    // An addon function for sceneLoaded
    // Adds extra functionality that works when the scene has been added to the hierarchy
    private void OnOptionsMenuLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == "OptionsMenu")
        {
            // sets up the OptionsMenu script so it can complement this one.
            _optionsMenu = GameObject.FindObjectOfType<OptionsMenu>();

            // onBackButton is a blank function similar to sceneLoaded
            _optionsMenu.onBackButton += CloseOptionsMenu;
            _optionsMenu.gameObject.SetActive(false);

            // this makes sure that there is no garbage left in sceneLoaded
            // once this script is destroyed.
            SceneManager.sceneLoaded -= OnOptionsMenuLoaded;
        }
    }
}
