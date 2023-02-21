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
        if (FindObjectOfType<Movement>() == null)
        {

        }
        else
        {
            vCamera.m_Follow = FindObjectOfType<Movement>().transform;
        }
    }
}