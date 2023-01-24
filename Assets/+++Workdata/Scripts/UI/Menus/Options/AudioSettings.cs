using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour, IDataPersistence
{
    public Slider thisSlider;
    [SerializeField] float masterVolume;
    [SerializeField] float musicVolume;
    [SerializeField] float sfxVolume;

    void Awake()
    {
        thisSlider = gameObject.GetComponent<Slider>();
    }

    void Start()
    {
        SetSliderValue("Master");
        SetSliderValue("Music");
        SetSliderValue("Sounds");
    }

    public void SetSliderValue(string value)
    {
        float sliderValue = thisSlider.value;

        if (value == "Master")
        {
            masterVolume = thisSlider.value;
            AkSoundEngine.SetRTPCValue("MasterVolume", masterVolume);
        }

        if (value == "Music")
        {
            musicVolume = thisSlider.value;
            AkSoundEngine.SetRTPCValue("MusicVolume", musicVolume);
        }

        if (value == "Sounds")
        {
            sfxVolume = thisSlider.value;
            AkSoundEngine.SetRTPCValue("SFXVolume", sfxVolume);
        }
    }

    public void LoadData(GameData data)
    {
        this.masterVolume = data.masterVolume;
        this.musicVolume = data.musicVolume;
        this.sfxVolume = data.sfxVolume;
    }

    /// <summary>
    /// saves the data upon exiting the game
    /// </summary>
    /// <param name="data"></param>
    public void SaveData(ref GameData data)
    {
        data.masterVolume = this.masterVolume;
        data.musicVolume = this.musicVolume;
        data.sfxVolume = this.sfxVolume;
    }
}
