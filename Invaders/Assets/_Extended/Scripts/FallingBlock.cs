using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    Animator anim;
    BoxCollider2D box;
    AudioSource audioSource;
    int playerLayer;

    public GameObject shadow;

    void Start()
    {

        playerLayer = LayerMask.NameToLayer("Player");
    }


    public void Fall()
    {
        Debug.Log("done");
    }
}