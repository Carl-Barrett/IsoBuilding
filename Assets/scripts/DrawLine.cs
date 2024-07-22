using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject startPoint; 
    public GameObject endPoint;   
    public Material lineMaterial; 

    private LineRenderer lineRenderer; 

    void Start()
    {
        
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = lineMaterial;
        lineRenderer.useWorldSpace = true;

        
        lineRenderer.enabled = false;
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            
            lineRenderer.enabled = true;
        }

        
        if (Input.GetKeyUp(KeyCode.F))
        {
            
            lineRenderer.enabled = false;
        }

        
        UpdateLinePositions();
    }

    
    void UpdateLinePositions()
    {
        if (lineRenderer.enabled && startPoint != null && endPoint != null)
        {
            
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, startPoint.transform.position);
            lineRenderer.SetPosition(1, endPoint.transform.position);
        }
    }
}
