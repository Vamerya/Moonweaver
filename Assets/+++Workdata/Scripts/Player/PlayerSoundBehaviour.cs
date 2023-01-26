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

    public void UpdatePlayerHealthstate()
    {
        if (playerInfos.playerHealthPercentage < .3)
            AkSoundEngine.SetState("PlayerHealthState", "LowLife");
        else
            AkSoundEngine.SetState("PlayerHealthState", "Healthy");
    }

    public void PlayFootstepAudio()
    {
        AkSoundEngine.PostEvent("Footsteps", this.gameObject);
    }

    public void PlayPlayerDamagedAudio()
    {
        AkSoundEngine.PostEvent("AsterHurt", this.gameObject);
    }

    public void PlayerSwordSwoosh()
    {
        AkSoundEngine.PostEvent("SwordSwoosh", this.gameObject);
    }

    public void PlayerLowLofeTrigger()
    {
        AkSoundEngine.PostTrigger("PlayerLowLife", this.gameObject);
    }
}
