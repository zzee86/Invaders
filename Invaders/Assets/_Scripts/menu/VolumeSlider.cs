using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;

    void Awake()
    {
        slider.value = PlayerPrefs.GetFloat("volume", AudioListener.volume);
    }
    void Start()
    {
        MasterVolumeManager.current.MasterVolume(slider.value);
        slider.onValueChanged.AddListener(val => MasterVolumeManager.current.MasterVolume(val));
    }
}
