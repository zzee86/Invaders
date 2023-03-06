using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class FollowPlayerController : MonoBehaviour
{
    CinemachineVirtualCamera vCamera;



    void Awake()
    {
        vCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        if (FindObjectOfType<PlayerController>() == null)
        {

        }
        else
        {
            vCamera.m_Follow = FindObjectOfType<PlayerController>().transform;
        }
    }
}