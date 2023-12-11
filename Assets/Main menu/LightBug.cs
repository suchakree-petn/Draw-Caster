using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightBug : MonoBehaviour
{
    public GameObject pointLightPrefab;
    public float lightIntensity = 1f;

    ParticleSystem particleSystem;
    ParticleSystem.Particle[] particles;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
    }

    void Update()
    {
        int count = particleSystem.GetParticles(particles);

        for (int i = 0; i < count; i++)
        {
            Vector3 particlePosition = particles[i].position;

            // Create or move a point light to the particle position
            GameObject pointLight = Instantiate(pointLightPrefab, particlePosition, Quaternion.identity);
            pointLight.GetComponent<Light2D>().intensity = lightIntensity;
        }
    }
}
