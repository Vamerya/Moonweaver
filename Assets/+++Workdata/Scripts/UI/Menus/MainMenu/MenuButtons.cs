using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    #region Variables
    [SerializeField] OptionsBehaviour optionsBehaviour;
    bool mainMenuActive;
    bool pauseMenuActive;
    bool optionsActive;
    [SerializeField] GameObject _mainMenu;
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
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync(sceneName);
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

    public void ToggleMainMenuOptions()
    {
        if(optionsActive)
        {
            _options.SetActive(false);
            _mainMenu.SetActive(true);
            optionsActive = false;
        }
        else if(!optionsActive)
        {
            _options.SetActive(true);
            _mainMenu.SetActive(false);
            optionsActive = true;
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
