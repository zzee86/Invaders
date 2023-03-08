using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    [SerializeField] private GameObject dustParticles;
    [SerializeField] private Vector3 Offset;
    [SerializeField] private GameObject shadow;

    void OnCollisionEnter2D(Collision2D collision)
    {

        AudioManager.PlayRockCrunch();
        Instantiate(dustParticles, transform.position + Offset, Quaternion.identity);
        Destroy(gameObject, 2);
        Destroy(shadow, 2);

    }
}