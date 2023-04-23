using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Photon.Pun;

public class AudioMultiplayer : MonoBehaviourPunCallbacks
{
    public AudioClip musicClip;
    public AudioMixerGroup musicGroup;

    AudioSource musicSource;


    // Start is called before the first frame update
    void Awake()
    {
        musicSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        musicSource.outputAudioMixerGroup = musicGroup;

        photonView.RPC("MultiplayerAudio", RpcTarget.All);
    }


    [PunRPC]
    void MultiplayerAudio()
    {
        musicSource.clip = musicClip;
        musicSource.loop = true;
        musicSource.Play();
    }
}
