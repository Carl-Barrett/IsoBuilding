using UnityEngine;

public class SnapPointCycle : MonoBehaviour
{
    private Transform[] snapPoints; // Array to store snap points
    private int currentSnapPointIndex = 0; // Index of the current snap point

    void Start()
    {
        // Find all children with the tag "SnapPoint"
        snapPoints = GetComponentsInChildren<Transform>();

        // Deactivate all snap points initially
        DeactivateAllSnapPoints();
    }

    void Update()
    {
        // Check for key press to cycle through snap points
        if (Input.GetKeyDown(KeyCode.W))
        {
            CycleSnapPoints();
        }
    }

    void DeactivateAllSnapPoints()
    {
        foreach (Transform snapPoint in snapPoints)
        {
            if (snapPoint.CompareTag("SnapPoint"))
            {
                snapPoint.gameObject.SetActive(false);
            }
        }
    }

    void CycleSnapPoints()
    {
        // Deactivate the current snap point
        snapPoints[currentSnapPointIndex].gameObject.SetActive(false);

        // Increment the snap point index
        currentSnapPointIndex++;
        if (currentSnapPointIndex >= snapPoints.Length)
        {
            currentSnapPointIndex = 0;
        }

        // Activate the next snap point
        snapPoints[currentSnapPointIndex].gameObject.SetActive(true);
    }
}
