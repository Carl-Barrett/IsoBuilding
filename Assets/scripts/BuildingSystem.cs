using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public GameObject cylinderPrefab; 
    public ModeSwitcher modeSwitcher; 
    public KeyCode uiToggleKey = KeyCode.L; 

    private Vector3 startMousePosition;
    private Vector3 endMousePosition;
    private bool isPlacing = false;
    private LineRenderer lineRenderer;
    private Camera mainCamera;
    private bool isUIToggled = false; 

    void Start()
    {
        mainCamera = Camera.main;

        
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.green;
        lineRenderer.positionCount = 0;

        Debug.Log("LineRenderer initialized.");
    }

    void Update()
    {
        
        if (Input.GetKeyDown(uiToggleKey))
        {
            isUIToggled = !isUIToggled; 
            Debug.Log("UI toggle state: " + isUIToggled);
        }

        if (modeSwitcher.isBuildingMode && isUIToggled)
        {
            HandleBuildingMode();
        }
    }

    void HandleBuildingMode()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            if (!isPlacing)
            {
                startMousePosition = GetMouseWorldPosition();
                isPlacing = true;
                Debug.Log("Started placing. Start position: " + startMousePosition);
            }
            else
            {
                endMousePosition = GetMouseWorldPosition();
                Debug.Log("Ending placing. End position: " + endMousePosition);
                CreateCylinder(startMousePosition, endMousePosition);
                isPlacing = false;
                lineRenderer.positionCount = 0;
                Debug.Log("LineRenderer reset.");
            }
        }

        
        if (isPlacing)
        {
            endMousePosition = GetMouseWorldPosition();
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, startMousePosition);
            lineRenderer.SetPosition(1, endMousePosition);
            Debug.Log("Drawing line. Start: " + startMousePosition + " End: " + endMousePosition);
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            float mouseYChange = Input.GetAxis("Mouse Y");
            float scalingFactor = 20.0f;
            mouseYChange *= scalingFactor;
            // Calculate the new y-axis position for the second point based on the start point
            float verticalOffset = mouseYChange * Time.deltaTime * 2f; 
            float newYPosition = endMousePosition.y + verticalOffset;

            
            Vector3 verticalPoint = new Vector3(startMousePosition.x, newYPosition, startMousePosition.z);

            return verticalPoint;
        }
        else
        {
            
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
            {
                Vector3 hitPoint = hitInfo.point;
                Debug.Log("Mouse world position: " + hitPoint);
                return hitPoint;
            }
            else
            {
                Debug.LogError("No collider hit by raycast.");
                return Vector3.zero;
            }
        }
    }


    void CreateCylinder(Vector3 start, Vector3 end)
    {
        
        Vector3 midpoint = (start + end) / 2;
        float length = Vector3.Distance(start, end);

        
        GameObject cylinder = Instantiate(cylinderPrefab, midpoint, Quaternion.identity);
        Debug.Log("Cylinder created at: " + midpoint);

        
        cylinder.transform.localScale = new Vector3(0.1f, length / 2, 0.1f);
        cylinder.transform.up = (end - start).normalized;
        Debug.Log("Cylinder scaled to length: " + length + " and rotated.");
    }
}
