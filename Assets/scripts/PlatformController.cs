using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public float lowerAmount = 1f; // Amount to lower the platform on collision (set in the inspector)
    public float raiseSpeed = 1f; // Speed at which the platform raises (set in the inspector)
    public float maxLoweredPosition = -5f; // Maximum lowered position of the platform (set in the inspector)
    public float maxRaisedPosition = 5f; // Maximum raised position of the platform (set in the inspector)
    public Color pressedColor = Color.red; // Color of the platform when pressed down (set in the inspector)
    private Color originalColor; // Original color of the platform

    private Vector3 initialPosition; // Initial position of the platform
    private bool isLowered = false; // Flag to track if the platform is lowered
    private Renderer platformRenderer; // Reference to the platform's renderer

    void Start()
    {
        // Store the initial position of the platform
        initialPosition = transform.position;
        // Get the renderer component of the platform
        platformRenderer = GetComponent<Renderer>();
        // Store the original color of the platform
        originalColor = platformRenderer.material.color;
    }

    // Called when a trigger collider enters
    void OnTriggerEnter(Collider other)
    {
        // Lower the platform
        LowerPlatform();
    }

    // Called when a trigger collider exits
    void OnTriggerExit(Collider other)
    {
        // If the platform is lowered and no trigger collider is inside, start raising the platform
        if (isLowered)
        {
            isLowered = false;
            StartCoroutine(RaisePlatform());
        }
    }

    // Lower the platform by the specified amount
    private void LowerPlatform()
    {
        // Calculate the new position after lowering
        Vector3 newPosition = transform.position - new Vector3(0f, lowerAmount, 0f);
        // Clamp the new position within the maximum lowered position
        newPosition.y = Mathf.Clamp(newPosition.y, maxLoweredPosition, initialPosition.y);
        // Update the platform position
        transform.position = newPosition;
        isLowered = true;
        // Change the platform color to pressed color
        platformRenderer.material.color = pressedColor;
    }

    // Coroutine to raise the platform back to its initial position
    private System.Collections.IEnumerator RaisePlatform()
    {
        while (transform.position.y < initialPosition.y)
        {
            // Calculate the new position after raising
            Vector3 newPosition = transform.position + new Vector3(0f, raiseSpeed * Time.deltaTime, 0f);
            // Clamp the new position within the maximum raised position
            newPosition.y = Mathf.Clamp(newPosition.y, initialPosition.y, maxRaisedPosition);
            // Update the platform position
            transform.position = newPosition;
            yield return null;
        }

        // Ensure platform is at the exact initial position
        transform.position = initialPosition;
        // Change the platform color back to original color
        platformRenderer.material.color = originalColor;
    }
}
