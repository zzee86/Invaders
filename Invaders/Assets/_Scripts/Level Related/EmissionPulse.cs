using UnityEngine;

public class EmissionPulse : MonoBehaviour
{
    [SerializeField] private float maxIntensity = 15f;
    private Material material;
    public int emissionProperty;
    [SerializeField] private float damping = 2f;




    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material;

        emissionProperty = Shader.PropertyToID("_EmissionColor");
    }

    void Update()
    {
        float emission = Mathf.PingPong(Time.time * damping, maxIntensity);

        Color finalColor = Color.white * emission;

        material.SetColor(emissionProperty, finalColor);
    }
}
