using UnityEngine;

public class EmissionPulse : MonoBehaviour
{
    public float maxIntensity = 15f;
    Material material;
    int emissionProperty;
    public float damping = 2f;




    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material;

        // ID of shader
        emissionProperty = Shader.PropertyToID("_EmissionColor");
    }

    void Update()
    {
        float emission = Mathf.PingPong(Time.time * damping, maxIntensity);

        Color finalColor = Color.white * emission;

        material.SetColor(emissionProperty, finalColor);
    }
}
