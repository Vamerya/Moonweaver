using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Controls 2D Lights in scene
/// </summary>
public class PulsingLightBehaviour : MonoBehaviour
{
    [SerializeField] Light2D _light;
    [SerializeField] float currentLightStrength, minLightStrength, maxLightStrength;
    [SerializeField] float intensityValue, timer;
    [SerializeField] bool increaseLightStrength;

    void Awake()
    {
        _light = gameObject.GetComponent<Light2D>();
    }

    void Start()
    {
        IncreaseLightStrength();
    }

    void Update()
    {
        currentLightStrength = _light.intensity;

        if (currentLightStrength <= minLightStrength)
        {
            increaseLightStrength = true;
            StartCoroutine(IncreaseLightStrength());
        }

        else if(currentLightStrength >= maxLightStrength)
        {
            increaseLightStrength = false;
            StartCoroutine(DecreaseLightStrength());
        }
    }

    IEnumerator IncreaseLightStrength()
    {
        while(increaseLightStrength)
        {
            _light.intensity += intensityValue;
            yield return new WaitForSecondsRealtime(timer);
        }
    }

    IEnumerator DecreaseLightStrength()
    {
        while(!increaseLightStrength)
        {
            _light.intensity -= intensityValue;
            yield return new WaitForSecondsRealtime(timer);
        }
    }
}
