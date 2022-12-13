using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarBehaviour : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] BossInfos bossInfos;
    [SerializeField] Image barImage;
    [SerializeField] Image fadingBarImage;
    Color fadingBarColor;
    const float fadingBarTimerMAX = 1f;
    float fadingBarTimer;
    void Awake()
    {
        barImage = transform.Find("Bar").GetComponent<Image>();
        fadingBarImage = transform.Find("FadingBar").GetComponent<Image>();
        fadingBarColor = fadingBarImage.color;
        fadingBarColor.a = 0f;
        fadingBarImage.color = fadingBarColor;
    }

    void Start()
    {
        SetStat(bossInfos.bossHealthPercentage);
    }

    void Update()
    {
        SetStat(bossInfos.bossHealthPercentage);

        if (fadingBarColor.a > 0)
        {
            fadingBarTimer -= Time.deltaTime;

            if (fadingBarTimer < 0)
            {
                float fadeAmount = 5f;
                fadingBarColor.a -= fadeAmount * Time.deltaTime;
                fadingBarImage.color = fadingBarColor;
            }
        }
    }

    public void SetStat(float statNormalized)
    {
        //sets fillamount of the healthbar to the normalized value of the Boss health
        barImage.fillAmount = statNormalized;
    }

    public void FadingBarBehaviour()
    {
        {
            if (fadingBarColor.a <= 0) //if the fade is invisible, it becomes visible, for the damage amount and fades away
            {
                fadingBarImage.fillAmount = barImage.fillAmount;
                fadingBarColor.a = 1;
                fadingBarImage.color = fadingBarColor;
                fadingBarTimer = fadingBarTimerMAX;
            }
            else //if the fade is already visible, the timer gets reset to have a bigger, fading bar
            {
                fadingBarColor.a = 1;
                fadingBarImage.color = fadingBarColor;
                fadingBarTimer = fadingBarTimerMAX;
                SetStat(bossInfos.bossHealthPercentage);
            }
        }
    }
}
