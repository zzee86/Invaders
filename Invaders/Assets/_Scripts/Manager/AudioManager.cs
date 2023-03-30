// All audio is held and controlled with this manager
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // Public instance to call any method
    public static AudioManager current;

    [Header("Ambient Audio")]
    public AudioClip ambientClip;		//The background ambient sound
    public AudioClip musicClip;         //The background music 

    [Header("Sting Clips")]
    public AudioClip levelStingClip;    //The sting played when the scene loads
    public AudioClip deathStingClip;    //The sting played when the player dies
    public AudioClip winStingClip;      //The sting played when the player wins
    public AudioClip orbStingClip;      //The sting played when an orb is collected
    public AudioClip doorOpenStingClip; //The sting played when the door opens

    [Header("Character Audio")]
    public AudioClip[] walkStepClips;   //The footstep sound effects
    public AudioClip[] crouchStepClips; //The crouching footstep sound effects
    public AudioClip deathClip;			//The player death sound effect
    public AudioClip jumpClip;          //The player jump sound effect

    public AudioClip jumpVoiceClip;     //The player jump voice
    public AudioClip deathVoiceClip;    //The player death voice
    public AudioClip orbVoiceClip;      //The player orb collection voice
    public AudioClip winVoiceClip;      //The player wins voice

    public AudioClip rock_crunch;

    [Header("Mixer Groups")]
    public AudioMixerGroup ambientGroup;//The ambient mixer group
    public AudioMixerGroup musicGroup;  //The music mixer group
    public AudioMixerGroup stingGroup;  //The sting mixer group
    public AudioMixerGroup playerGroup; //The player mixer group
    public AudioMixerGroup voiceGroup;  //The voice mixer group

    AudioSource ambientSource;			//Reference to the generated ambient Audio Source
    AudioSource musicSource;            //Reference to the generated music Audio Source
    AudioSource stingSource;            //Reference to the generated sting Audio Source
    AudioSource playerSource;           //Reference to the generated player Audio Source
    AudioSource voiceSource;            //Reference to the generated voice Audio Source

    // Create Singleton
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

        //Being playing the game audio
        StartLevelAudio();
    }

    void StartLevelAudio()
    {
        //Set the clip for ambient audio, tell it to loop, and then tell it to play
        current.ambientSource.clip = current.ambientClip;
        current.ambientSource.loop = true;
        current.ambientSource.Play();

        //Set the clip for music audio, tell it to loop, and then tell it to play
        current.musicSource.clip = current.musicClip;
        current.musicSource.loop = true;
        current.musicSource.Play();

        //Play the audio that repeats whenever the level reloads
        PlaySceneRestartAudio();
    }

    public static void PlayFootstepAudio()
    {
        //If there is no current AudioManager or the player source is already playing
        //a clip, exit 
        if (current == null || current.playerSource.isPlaying)
            return;

        //Pick a random footstep sound
        int index = Random.Range(0, current.walkStepClips.Length);

        //Set the footstep clip and tell the source to play
        current.playerSource.clip = current.walkStepClips[index];
        current.playerSource.Play();
    }

    public static void PlayCrouchFootstepAudio()
    {
        //If there is no current AudioManager or the player source is already playing
        //a clip, exit 
        if (current == null || current.playerSource.isPlaying)
            return;

        //Pick a random crouching footstep sound
        int index = Random.Range(0, current.crouchStepClips.Length);

        //Set the footstep clip and tell the source to play
        current.playerSource.clip = current.crouchStepClips[index];
        current.playerSource.Play();
    }

    public static void PlayJumpAudio()
    {
        //If there is no current AudioManager, exit
        if (current == null)
            return;

        //Set the jump SFX clip and tell the source to play
        current.playerSource.clip = current.jumpClip;
        current.playerSource.Play();

        //Set the jump voice clip and tell the source to play
        current.voiceSource.clip = current.jumpVoiceClip;
        current.voiceSource.Play();
    }
    public static void PlayRockCrunch()
    {
        //If there is no current AudioManager, exit
        if (current == null)
            return;

        //Set the jump SFX clip and tell the source to play
        current.playerSource.clip = current.rock_crunch;
        current.playerSource.Play();

        //Set the jump voice clip and tell the source to play
        current.voiceSource.clip = current.rock_crunch;
        current.voiceSource.Play();
    }
    public static void PlayDeathAudio()
    {
        //If there is no current AudioManager, exit
        if (current == null)
            return;

        //Set the death SFX clip and tell the source to play
        current.playerSource.clip = current.deathClip;
        current.playerSource.Play();

        //Set the death voice clip and tell the source to play
        current.voiceSource.clip = current.deathVoiceClip;
        current.voiceSource.Play();

        //Set the death sting clip and tell the source to play
        current.stingSource.clip = current.deathStingClip;
        current.stingSource.Play();
    }


    public static void PlayOrbCollectionAudio()
    {
        //If there is no current AudioManager, exit
        if (current == null)
            return;

        //Set the orb sting clip and tell the source to play
        current.stingSource.clip = current.orbStingClip;
        current.stingSource.Play();

        //Set the orb voice clip and tell the source to play
        current.voiceSource.clip = current.orbVoiceClip;
        current.voiceSource.Play();
    }

    public static void PlaySceneRestartAudio()
    {
        //If there is no current AudioManager, exit
        if (current == null)
            return;

        //Set the level reload sting clip and tell the source to play
        current.stingSource.clip = current.levelStingClip;
        current.stingSource.Play();
    }

    public static void PlayDoorOpenAudio()
    {
        //If there is no current AudioManager, exit
        if (current == null)
            return;

        //Set the door open sting clip and tell the source to play
        current.stingSource.clip = current.doorOpenStingClip;
        current.stingSource.PlayDelayed(1f);
    }

    public static void PlayWonAudio()
    {
        if (current == null)
            return;

        //Stop the ambient sound
        current.ambientSource.Stop();

        //Set the player won voice clip and tell the source to play
        current.voiceSource.clip = current.winVoiceClip;
        current.voiceSource.Play();

        //Set the player won sting clip and tell the source to play
        current.stingSource.clip = current.winStingClip;
        current.stingSource.Play();
    }
}
