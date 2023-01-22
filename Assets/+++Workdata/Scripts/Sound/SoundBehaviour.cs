using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundInPauseMenu : MonoBehaviour
{
    void OnEnable()
    {
        AkSoundEngine.SetState("MenuState", "Paused");
    }

    void OnDisable()
    {
        AkSoundEngine.SetState("MenuState", "Unpaused");
    }
}
