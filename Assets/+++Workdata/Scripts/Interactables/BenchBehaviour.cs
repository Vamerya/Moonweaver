using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchBehaviour : MonoBehaviour
{
    public Animator anim;
    public GameObject cameraLookAtPoint;
    bool increaseLight;
    public float focalLength;

    void Awake()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        anim.SetBool("increaseLight", increaseLight);
    }

    public void IncreaseLightStrength()
    {
        anim.SetTrigger("increaseStrength");
        increaseLight = true;
    }

    public void DecreaseLightStrength()
    {
        anim.SetTrigger("decreaseStrength");
        increaseLight = false;
    }
}
