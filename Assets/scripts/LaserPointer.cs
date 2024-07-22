using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    public Transform groundPlane; // Reference to the ground plane
    public GameObject cat; // Reference to the object (e.g., cat) to draw a line to

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Cast a ray from the mouse position in screen space to the ground plane
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity) && hitInfo.transform == groundPlane)
        {
            // If the ray hits the ground plane, draw a red line between the cat and the hit point
            Vector3 catPosition = cat.transform.position;
            Vector3 hitPoint = hitInfo.point;

            Debug.DrawLine(catPosition, hitPoint, Color.red);
        }
    }
}
