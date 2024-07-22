using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera topViewCamera;        // Reference to the top view camera
    public Camera elevationViewCamera;  // Reference to the elevation view camera
    public Camera sideViewCamera;       // Reference to the side view camera
    public KeyCode cameraSwitchKey = KeyCode.Q; // Key to switch between camera views
    
    
    private Vector3 originCamOne;
    private Vector3 originCamTwo;
    private Vector3 originCamThree;

    private int currentCameraIndex = 2; // Index to track current camera view

    void Start()
    {
        // Disable all cameras initially
        topViewCamera.gameObject.SetActive(false);
        elevationViewCamera.gameObject.SetActive(false);
        sideViewCamera.gameObject.SetActive(true);
        originCamOne = sideViewCamera.transform.position;
        originCamTwo = topViewCamera.transform.position;
        originCamThree = elevationViewCamera.transform.position;
    }

    void Update()
    {
        // Check if in building mode and camera switch key is pressed
        if (GetComponent<ModeSwitcher>().isBuildingMode && Input.GetKeyDown(cameraSwitchKey))
        {
            // Switch to the next camera view
            currentCameraIndex = (currentCameraIndex + 1) % 3;
            SwitchCameraView(currentCameraIndex);
        }
    }

    // Method to switch between different camera views
    void SwitchCameraView(int index)
    {
        // Disable all cameras
        topViewCamera.gameObject.SetActive(false);
        elevationViewCamera.gameObject.SetActive(false);
        sideViewCamera.gameObject.SetActive(false);

        if (Input.GetKeyDown(cameraSwitchKey))
        {
            topViewCamera.transform.position = originCamTwo;
            elevationViewCamera.transform.position = originCamThree;
            sideViewCamera.transform.position = originCamOne;
        }

        // Enable the camera based on the index
        switch (index)
        {
            case 0:
                topViewCamera.gameObject.SetActive(true);
                

                break;
            case 1:
                elevationViewCamera.gameObject.SetActive(true);
                

                break;
            case 2:
                sideViewCamera.gameObject.SetActive(true);
                
                break;
        }
    }

    
}

