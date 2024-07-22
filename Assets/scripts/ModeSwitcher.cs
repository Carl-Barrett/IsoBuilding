using UnityEngine;

public class ModeSwitcher : MonoBehaviour
{
    public KeyCode gameplayModeKey = KeyCode.Alpha1; // Key to switch to gameplay mode
    public KeyCode buildingModeKey = KeyCode.Alpha2; // Key to switch to building mode
    public KeyCode uiModeKey = KeyCode.Alpha3;       // Key to switch to UI mode

    public bool isBuildingMode { get; private set; } // Flag to track if in building mode
    public Animator characterAnimator;               // Reference to the character animator
    public GameObject deskObject; // Reference to the desk object to turn on/off

    private Vector3 originCamOne;
    private Vector3 originCamTwo;
    private Vector3 originCamThree;


    public GameObject cameraIso;
    public GameObject cameraTop;
    public GameObject cameraElevation;

    private CharacterController characterController;
    public// Reference to the character controller script

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        originCamOne = cameraIso.transform.position;
        originCamTwo = cameraTop.transform.position;
        originCamThree = cameraElevation.transform.position;

        // Initialize animator reference if not set in the inspector
        if (characterAnimator == null)
        {
            characterAnimator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        // Check if the gameplay mode key is pressed
        if (Input.GetKeyDown(gameplayModeKey))
        {
            // Switch to gameplay mode
            SwitchToGameplayMode();
            cameraIso.SetActive(true);
            cameraTop.SetActive(false);
            cameraElevation.SetActive(false);
            

        }

        // Check if the building mode key is pressed
        if (Input.GetKeyDown(buildingModeKey))
        {
            // Switch to building mode
            SwitchToBuildingMode();
            
        }

        // Check if the UI mode key is pressed
        if (Input.GetKeyDown(uiModeKey))
        {
            // Switch to UI mode
            SwitchToUIMode();
        }
    }

    // Method to switch to gameplay mode
    void SwitchToGameplayMode()
    {
        //cameraIso.transform.position = originCamOne;
        //cameraTop.transform.position = originCamTwo;
       //cameraElevation.transform.position = originCamThree;
        isBuildingMode = false;
        // Enable character controller
        characterController.enabled = true;
        // Set isDesigning parameter to false in animator
        if (characterAnimator != null)
        {
            characterAnimator.SetBool("isDesigning", false);
        }
        // Deactivate desk object
        if (deskObject != null)
        {
            deskObject.SetActive(false);
        }
    }

    // Method to switch to building mode
    void SwitchToBuildingMode()
    {
        //cameraIso.transform.position = originCamOne;
        //cameraTop.transform.position = originCamTwo;
        //cameraElevation.transform.position = originCamThree;
        isBuildingMode = true;
        // Disable character controller
        characterController.enabled = false;
        // Set isDesigning parameter to true in animator
        if (characterAnimator != null)
        {
            characterAnimator.SetBool("isDesigning", true);
        }
        // Activate desk object
        if (deskObject != null)
        {
            deskObject.SetActive(true);
        }
    }

    // Method to switch to UI mode
    void SwitchToUIMode()
    {
        // Placeholder for UI mode functionality
        Debug.Log("Switched to UI mode");
    }
}
