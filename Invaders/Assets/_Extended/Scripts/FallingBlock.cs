using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FallingBlock : MonoBehaviour
{
    [SerializeField] private GameObject dustParticles;
    [SerializeField] private Vector3 Offset;
    private Rigidbody2D body;

    int playerLayer;
    
    [SerializeField] private float destroyTimer = 2f;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerLayer = LayerMask.NameToLayer("Player");

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            AudioManager.PlayRockCrunch();
            Instantiate(dustParticles, transform.position + Offset, Quaternion.identity);

            StartCoroutine(fall());
        }
    }

    IEnumerator fall()
    {
        yield return new WaitForSeconds(1f);
        body.isKinematic = false;
        Destroy(gameObject, destroyTimer);
    }

}