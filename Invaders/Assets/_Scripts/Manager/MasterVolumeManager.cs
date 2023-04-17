using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class MasterVolumeManager : MonoBehaviour
{
    public static MasterVolumeManager current;

    void Awake()
    {
        if (current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }
        current = this;

        // Persist the object between scene reloads and new scenes
        // And OnSceneLoaded() destroys it when the main menu scene is active
        DontDestroyOnLoad(gameObject);

        AudioListener.volume = PlayerPrefs.GetFloat("volume", AudioListener.volume);

    }
    public void MasterVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("volume", AudioListener.volume);
    }
}
