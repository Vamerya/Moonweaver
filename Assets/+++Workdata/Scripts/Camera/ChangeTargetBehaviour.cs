using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTargetBehaviour : MonoBehaviour
{
    [SerializeField] Cinemachine.CinemachineVirtualCamera vcam;
    [SerializeField] Animator anim;
    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerSittingBehaviour sittingBehaviour;
    [SerializeField] Transform playerTransform;
    [SerializeField] float defaultFocalLength;
    [SerializeField] bool zoomOut;

    void Awake()
    {
        vcam = gameObject.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        anim = gameObject.GetComponent<Animator>();
        defaultFocalLength = vcam.m_Lens.OrthographicSize;
    }

    void Update()
    {
        anim.SetBool("zoomOut", zoomOut);
    }
    public void ChangeFollowTarget(GameObject _target)
    {
        vcam.Follow = _target.transform;
        anim.SetTrigger("increaseFocalLength");
        zoomOut = true;
    }

    public void ResetFollowTarget()
    {
        vcam.Follow = playerTransform;
        anim.SetTrigger("decreaseFocalLength");
        zoomOut = false;
    }
}
