using UnityEngine;

public class PrefabSetup : MonoBehaviour
{
    void Start()
    {
        // Find all children with the tag "SnapPoint"
        Transform[] snapPoints = GetComponentsInChildren<Transform>();
        foreach (Transform snapPoint in snapPoints)
        {
            if (snapPoint.CompareTag("SnapPoint"))
            {
                // Set the origin of the prefab to the position of the first snap point found
                transform.position = snapPoint.position;
                break;
            }
        }
    }
}
