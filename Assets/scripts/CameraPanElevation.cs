using UnityEngine;

public class CameraPanElevation : MonoBehaviour
{
    public float panSpeed = 2f; // Speed of camera panning

    private bool isPanning = false; // Flag to track if panning is active
    private Vector3 lastMousePosition; // Last mouse position during panning
    private ModeSwitcher modeSwitcher; // Reference to the ModeSwitcher script

    void Start()
    {
        // Find and store a reference to the ModeSwitcher script
        modeSwitcher = FindObjectOfType<ModeSwitcher>();
        if (modeSwitcher == null)
        {
            Debug.LogError("ModeSwitcher script not found in the scene!");
        }
    }

    void Update()
    {
        // Check if the game is in build mode
        if (modeSwitcher != null && modeSwitcher.isBuildingMode)
        {
            // Check if middle mouse button is pressed down
            if (Input.GetMouseButtonDown(2))
            {
                isPanning = true;
                lastMousePosition = Input.mousePosition;
            }

            // Check if middle mouse button is released
            if (Input.GetMouseButtonUp(2))
            {
                isPanning = false;
            }

            // If panning is active, update camera position based on mouse drag
            if (isPanning)
            {
                Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;

                // Calculate movement in world space
                Vector3 pan = new Vector3(deltaMousePosition.x, -deltaMousePosition.y, 0) * panSpeed * Time.deltaTime;

                // Apply movement to camera's position
                transform.Translate(pan, Space.Self);

                lastMousePosition = Input.mousePosition;
            }
        }
    }
}
