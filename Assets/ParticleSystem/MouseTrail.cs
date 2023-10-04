using UnityEngine;

public class MouseTrail : MonoBehaviour
{
    public new ParticleSystem particleSystem;
    private ParticleSystem.EmissionModule emissionModule;
    private ParticleSystem.ShapeModule shapeModule;

    private void Start()
    {
        emissionModule = particleSystem.emission;
        shapeModule = particleSystem.shape;
    }

    private void Update()
    {
        // Get the mouse position in world coordinates
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Set the Particle System's position to the mouse position
        transform.position = mousePosition;

        // Enable emission to create particles
        emissionModule.enabled = true;

        // Adjust the shape module to control the particle trail
        shapeModule.rotation = new Vector3(90f, 0f, 0f);
        shapeModule.radius = 0.1f; // Adjust this value to control the trail width

        // Disable emission after a short delay to create a trail effect
        emissionModule.enabled = false;
    }
}
