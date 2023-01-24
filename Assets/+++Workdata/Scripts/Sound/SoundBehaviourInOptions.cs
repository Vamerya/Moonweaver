using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBehaviourInOptions : MonoBehaviour
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
