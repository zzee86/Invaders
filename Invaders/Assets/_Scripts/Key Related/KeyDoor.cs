using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] private Key.KeyType keyType;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public Key.KeyType GetKeyType()
    {
        return keyType;
    }
    public void OpenDoor()
    {
       anim.SetTrigger("Open");
       AudioManager.PlayDoorOpenAudio();
    }

}
