using UnityEngine;

public class TopViewCursor : MonoBehaviour
{
    public GameObject cursorImage; // Reference to the cursor image object

    private Camera topViewCamera; // Reference to the top view camera

    void Start()
    {
        // Find and store a reference to the top view camera
        topViewCamera = GetComponent<Camera>();
        if (topViewCamera == null)
        {
            Debug.LogError("Top view camera not found on the object!");
            return;
        }
    }

    void Update()
    {
        // Activate cursor image when the top view camera is active
        if (topViewCamera.enabled)
        {
            cursorImage.SetActive(true);

            // Cast a ray from the camera to detect the collider under the cursor
            Ray ray = topViewCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                // Place the cursor image on top of the collider hit point
                cursorImage.transform.position = hitInfo.point + Vector3.up * 0.1f; // Adjust the height as needed
            }
            else
            {
                // If no collider is hit, place the cursor image at a default position
                cursorImage.transform.position = new Vector3(0, 0.1f, 0); // Default height position
            }
        }
        else
        {
            // Deactivate cursor image when the top view camera is inactive
            cursorImage.SetActive(false);
        }
    }
}
