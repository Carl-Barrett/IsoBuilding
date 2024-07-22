using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float normalSpeed = 5f; 
    public float sprintSpeed = 10f; 
    public float groundSize = 10f; 
    public float stopDistance = 1f; 
    public float sprintTransitionSpeed = 5f; 
    public KeyCode sprintKey = KeyCode.LeftShift; 
    public Animator characterAnimator; 

    private Camera mainCamera;
    private Plane groundPlane;
    private Rigidbody rb;
    private bool isSprinting = false;

    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();

        
        groundPlane = new Plane(Vector3.up, Vector3.zero);
        Vector3 planeSize = new Vector3(groundSize, 1f, groundSize);
        groundPlane.Set3Points(Vector3.zero, Vector3.right * groundSize, Vector3.forward * groundSize);

        
        rb.freezeRotation = true;
    }

    void Update()
    {
        Vector3 targetPosition = Vector3.zero;
        float distanceToMouseCheck = 0f;

        
        if (Input.GetKeyDown(sprintKey))
        {
            
            targetPosition = GetTargetPosition();
            distanceToMouseCheck = Vector3.Distance(targetPosition, transform.position);
            bool isMoving = distanceToMouseCheck > stopDistance;

            if (isMoving)
            {
                isSprinting = true;
            }
            else
            {
                characterAnimator.SetBool("isDancing", true); 
            }
        }
        
        else if (Input.GetKeyUp(sprintKey))
        {
            isSprinting = false;
            characterAnimator.SetBool("isDancing", false); 
        }

        
        targetPosition = GetTargetPosition();
        float distanceToMouse = Vector3.Distance(targetPosition, transform.position);
        if (distanceToMouse > stopDistance)
        {
            
            MoveCharacter(targetPosition);
        }
        else
        {
            
            StopCharacter();
        }
    }

    Vector3 GetTargetPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (groundPlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        return transform.position;
    }

    void MoveCharacter(Vector3 targetPosition)
    {
        
        float targetSpeed = isSprinting ? sprintSpeed : normalSpeed;

        
        float step = sprintTransitionSpeed * Time.deltaTime;
        Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, step * targetSpeed);

        
        rb.velocity = (newPosition - transform.position) / Time.deltaTime;

        
        if (targetPosition != transform.position)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up); 
            lookRotation.x = 0f; 
            lookRotation.z = 0f; 
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.15f); 
        }

        
        float movementMagnitude = (targetPosition - transform.position).magnitude;
        characterAnimator.SetBool("isMoving", true);
        characterAnimator.SetFloat("MovementSpeed", movementMagnitude);
        characterAnimator.SetBool("isSprinting", isSprinting);
    }

    void StopCharacter()
    {
        rb.velocity = Vector3.zero;
        characterAnimator.SetBool("isMoving", false);
        characterAnimator.SetFloat("MovementSpeed", 0f);
        characterAnimator.SetBool("isSprinting", false);
    }
}
