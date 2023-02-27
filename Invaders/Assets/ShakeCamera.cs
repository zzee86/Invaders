using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeCamera : MonoBehaviour
{
    CinemachineVirtualCamera cinemachineVirtualCamera;
    CinemachineBasicMultiChannelPerlin noise;

    void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        noise = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

    }

    public void Shake()
    {
        noise.m_AmplitudeGain = 1.2f;
        noise.m_FrequencyGain = 1;
    }

}
