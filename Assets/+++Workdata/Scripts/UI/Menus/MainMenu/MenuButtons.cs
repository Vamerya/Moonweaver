using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    #region Variables
    [SerializeField] DataPersistenceManager dataPersistenceManager;
    [SerializeField] OptionsBehaviour optionsBehaviour;
    bool mainMenuActive;
    bool pauseMenuActive;
    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _saveFiles;
    [SerializeField] GameObject _options;
    [SerializeField] GameObject _pauseMenu;
    #endregion

    void Awake()
    {
        
    }
    void Start()
    {
        Time.timeScale = 1f;
        mainMenuActive = true;
        pauseMenuActive = false;
    }
    public void LoadScene(string sceneName)
    {
        //switches between scenes
        var activeScene = SceneManager.GetActiveScene().ToString();
        if (activeScene == "MainGame")
        {
            dataPersistenceManager.SaveGame();
            StartCoroutine(WaitSoThisFlipFinallySaves());
            SceneManager.LoadScene(sceneName);
        }
        else
            SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        //exits the game
        Application.Quit();
    }

    public void ActivateMainMenu()
    {
        _mainMenu.SetActive(true);
    }
    public void TogglePauseMenu()
    {
        if (!pauseMenuActive)
        {
            Time.timeScale = 0f;
            _pauseMenu.SetActive(true);
            pauseMenuActive = true;
            _options.SetActive(false);
            optionsBehaviour.CloseAllOptions();
        }
        else if (pauseMenuActive)
        {
            Time.timeScale = 1f;
            _pauseMenu.SetActive(false);
            pauseMenuActive = false;
        }
    }

    public void ResumeButton()
    {
        //unfreezes game, actiavtes playerinput, enable enemy AI and disables pause menu
        Time.timeScale = 1f;
        _pauseMenu.SetActive(false);
        gameObject.GetComponent<PlayerController>().inGameInputActions.Enable();
    }

    //swaps between the main menu buttons and save files
    public void ToggleSaves()
    {
        if (mainMenuActive)
        {
            _mainMenu.SetActive(false);
            _saveFiles.SetActive(true);
            mainMenuActive = false;
        }
        else if (!mainMenuActive)
        {
            _mainMenu.SetActive(true);
            _saveFiles.SetActive(false);
            mainMenuActive = true;
        }
    }

    public void ToggleOptions()
    {
        if (pauseMenuActive)
        {
            _mainMenu.SetActive(false);
            _options.SetActive(true);
            pauseMenuActive = false;
        }
        else if (!pauseMenuActive)
        {
            _mainMenu.SetActive(true);
            _options.SetActive(false);
            pauseMenuActive = false;
        }
    }

    /// <summary>
    /// this clusterfuck is just here so that the savni system finally saves when scenes are switched
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitSoThisFlipFinallySaves()
    {
        yield return new WaitForSecondsRealtime(2f);
    }
}
