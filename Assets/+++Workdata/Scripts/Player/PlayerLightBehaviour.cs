using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightBehaviour : MonoBehaviour
{
    public Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
}
