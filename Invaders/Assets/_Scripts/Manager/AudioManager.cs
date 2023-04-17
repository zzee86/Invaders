using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{
    public static AudioManager current;

    [Header("Ambient Audio")]
    public AudioClip ambientClip;
    public AudioClip musicClip;

    [Header("Sting Clips")]
    public AudioClip levelStingClip;
    public AudioClip winStingClip;
    public AudioClip orbStingClip;
    public AudioClip doorOpenStingClip;

    [Header("Character Audio")]
    public AudioClip orbVoiceClip;
    public AudioClip rock_crunch;


    [Header("Mixer Groups")]
    public AudioMixerGroup ambientGroup;
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup stingGroup;
    public AudioMixerGroup playerGroup;
    public AudioMixerGroup voiceGroup;

    AudioSource ambientSource;
    AudioSource musicSource;
    AudioSource stingSource;
    AudioSource playerSource;
    AudioSource voiceSource;

    void Awake()
    {
        if (current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }
        current = this;

        DontDestroyOnLoad(gameObject);



        //Generate the Audio Source "channels" for our game's audio
        ambientSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        musicSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        stingSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        playerSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        voiceSource = gameObject.AddComponent<AudioSource>() as AudioSource;

        //Assign each audio source to its respective mixer group so that it is
        //routed and controlled by the audio mixer
        ambientSource.outputAudioMixerGroup = ambientGroup;
        musicSource.outputAudioMixerGroup = musicGroup;
        stingSource.outputAudioMixerGroup = stingGroup;
        playerSource.outputAudioMixerGroup = playerGroup;
        voiceSource.outputAudioMixerGroup = voiceGroup;


        StartLevelAudio();
    }

    void StartLevelAudio()
    {

        current.ambientSource.clip = current.ambientClip;
        current.ambientSource.loop = true;
        current.ambientSource.Play();


        current.musicSource.clip = current.musicClip;
        current.musicSource.loop = true;
        current.musicSource.Play();


        PlaySceneRestartAudio();
    }

    public static void PlayRockCrunch()
    {

        if (current == null)
            return;


        current.playerSource.clip = current.rock_crunch;
        current.playerSource.Play();


        current.voiceSource.clip = current.rock_crunch;
        current.voiceSource.Play();
    }

    public static void PlayOrbCollectionAudio()
    {

        if (current == null)
            return;


        current.stingSource.clip = current.orbStingClip;
        current.stingSource.Play();


        current.voiceSource.clip = current.orbVoiceClip;
        current.voiceSource.Play();
    }

    public static void PlaySceneRestartAudio()
    {

        if (current == null)
            return;


        current.stingSource.clip = current.levelStingClip;
        current.stingSource.Play();
    }

    public static void PlayDoorOpenAudio()
    {

        if (current == null)
            return;


        current.stingSource.clip = current.doorOpenStingClip;
        current.stingSource.PlayDelayed(1f);
    }

    public static void PlayWonAudio()
    {
        if (current == null)
            return;


        current.ambientSource.Stop();


        current.stingSource.clip = current.winStingClip;
        current.stingSource.Play();
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

    // Destroy GameManager On MainMenu To Replay Completed Level
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Scene.name to check which scene was loaded
        if (scene.name == "MainMenu")
        {

            // Destroy the GameManager object
            Destroy(gameObject);
        }
    }


}
