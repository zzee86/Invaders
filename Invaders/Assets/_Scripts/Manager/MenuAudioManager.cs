using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuAudioManager : MonoBehaviour
{
    public static MenuAudioManager current;

    public AudioClip backgroundClip;
    public AudioMixerGroup ambientGroup;
    AudioSource ambientSource;



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

        ambientSource = gameObject.AddComponent<AudioSource>() as AudioSource;

        StartLevelAudio();
    }
    void StartLevelAudio()
    {
        //Set the clip for ambient audio, tell it to loop, and then tell it to play
        current.ambientSource.clip = current.backgroundClip;
        current.ambientSource.loop = true;
        current.ambientSource.Play();
    }



    // OnEnable called everytime the gameobject is called
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    // Destroy GameManager 
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Scene.name to check which scene was loaded
        // if (scene.name != "MainMenu" || scene.name != "LevelSelector" || scene.name != "Lobby" || scene.name != "Login")
        // {

        //     // Destroy the GameManager object
        //     Destroy(gameObject);
        // }
        if (scene.name == "Level1" || scene.name == "Level2" || scene.name == "Multiplayer1" || scene.name == "Multiplayer2")
        {

            // Destroy the GameManager object
            Destroy(gameObject);
        }
    }

    public void DestroyManager()
    {
        Destroy(gameObject);
    }
}