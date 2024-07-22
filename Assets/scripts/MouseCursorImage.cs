using UnityEngine;

public class MouseCursorImage : MonoBehaviour
{
    public GameObject cursorImage; 

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Cast a ray from the mouse position in screen space to the ground plane
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
        {
            
            Vector3 hitPoint = hitInfo.point;
            hitPoint.y = 0; 

            cursorImage.SetActive(true);
            cursorImage.transform.position = hitPoint;
        }
        else
        {
            cursorImage.SetActive(false);
        }
    }
}
