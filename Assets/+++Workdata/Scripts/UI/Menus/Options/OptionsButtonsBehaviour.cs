using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsBehaviour : MonoBehaviour
{
    [SerializeField] GameObject _generalSettings;
    [SerializeField] GameObject _graphicsSettings;
    [SerializeField] GameObject _soundSettings;
    [SerializeField] GameObject _controlSettings;

    public void ShowGeneralSetting()
    {
        _generalSettings.SetActive(true);
        _graphicsSettings.SetActive(false);
        _soundSettings.SetActive(false);
        _controlSettings.SetActive(false);
    }

    public void ShowGraphicSettings()
    {
        _generalSettings.SetActive(false);
        _graphicsSettings.SetActive(true);
        _soundSettings.SetActive(false);
        _controlSettings.SetActive(false);
    }

    public void ShowAudioSettings()
    {
        _generalSettings.SetActive(false);
        _graphicsSettings.SetActive(false);
        _soundSettings.SetActive(true);
        _controlSettings.SetActive(false);
    }

    public void ShowControls()
    {
        _generalSettings.SetActive(false);
        _graphicsSettings.SetActive(false);
        _soundSettings.SetActive(false);
        _controlSettings.SetActive(true);
    }
}
