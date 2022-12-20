using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShrineMenuBehaviour : MonoBehaviour
{
    [SerializeField] PlayerInfos playerInfos;
    [SerializeField] PlayerController playerController;
    [SerializeField] ShrineManager shrineManager;
    [SerializeField] TextMeshProUGUI _collectedFragments;
    [SerializeField] GameObject _shrineMenu;
    [SerializeField] GameObject _levelUpMenu;
    [SerializeField] GameObject _statMenu;


    void Update()
    {
        DisplayAmountOfCollectedFragments();
    }

    /// <summary>
    /// Opens the levelUp UI
    /// </summary>
    public void OpenLevelUpMenu()
    {
        _shrineMenu.SetActive(false);
        _levelUpMenu.SetActive(true);
    }

    /// <summary>
    /// Opens the player stat UI
    /// </summary>
    public void OpenStatMenu()
    {
        _shrineMenu.SetActive(false);
        _statMenu.SetActive(true);
    }

    /// <summary>
    /// Stores the fragments the player has in their inventory into the main Shrine and sets the amount of collected fragments from the player to 0
    /// </summary>
    public void StoreFragments()
    {
        shrineManager.StoreMoonFragments();
    }

    /// <summary>
    /// Closes all the other UIs and open the mainShrineMenu again 
    /// </summary>
    public void ReturnToShrineMenu()
    {
        _levelUpMenu.SetActive(false);
        _statMenu.SetActive(false);
        _shrineMenu.SetActive(true);
    }

    /// <summary>
    /// Closes the shrineMenu
    /// </summary>
    public void CloseShrineMenu()
    {
        _shrineMenu.SetActive(false);
        playerController.inGameInputActions.Enable();
    }

    /// <summary>
    /// Shows how many fragments the player has already stored at the main Shrine
    /// </summary>
    void DisplayAmountOfCollectedFragments()
    {
        _collectedFragments.text = "You have stored " + shrineManager.storedMoonFragments + " fragments of the Moon so far";
    }
}
