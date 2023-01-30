using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundBehaviour : MonoBehaviour
{
    [SerializeField] PlayerInfos playerInfos;


    void Awake()
    {
        playerInfos = GetComponentInParent<PlayerInfos>();
    }

    void Update()
    {
        UpdatePlayerHealthstate();
    }

    /// <summary>
    /// so sorry for smacking this into the update method but it's the most reliable way
    /// </summary>
    public void UpdatePlayerHealthstate()
    {
        if (playerInfos.playerHealthPercentage < .3)
            AkSoundEngine.SetState("PlayerHealthState", "LowLife");
        else
            AkSoundEngine.SetState("PlayerHealthState", "Healthy");
    }

    /// <summary>
    /// plays the footstep audio
    /// called via animation events
    /// </summary>
    public void PlayFootstepAudio()
    {
        AkSoundEngine.PostEvent("Footsteps", this.gameObject);
    }

    /// <summary>
    /// plays the aster hurt audio
    /// </summary>
    public void PlayPlayerDamagedAudio()
    {
        AkSoundEngine.PostEvent("AsterHurt", this.gameObject);
    }

    /// <summary>
    /// plays the sword swoosh audio
    /// called via animation events
    /// </summary>
    public void PlayerSwordSwoosh()
    {
        AkSoundEngine.PostEvent("SwordSwoosh", this.gameObject);
    }

    /// <summary>
    /// this is supposed to play a heart beat sound in the tempo of the current soundtrack
    /// didn't manage to get it to work yet
    /// </summary>
    public void PlayerLowLofeTrigger()
    {
        AkSoundEngine.PostTrigger("PlayerLowLife", this.gameObject);
    }
}
