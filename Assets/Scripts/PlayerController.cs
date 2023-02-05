using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Box data
    // Simon says data Question/answer
    // Radiation amount

    public bool CanMove { get; set; } = true;
    private bool IsSprinting => canSprint && Input.GetKey(sprintKey);
    private bool ShouldJump => Input.GetKeyDown(jumpKey) && characterController.isGrounded;

    [Header("Functional options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canUseHeadBob = true;
    [SerializeField] private bool canInteract = true;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    [Header("Movement parameters")]
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float sprintSpeed = 8f;

    [Header("Jump parameters")]
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float gravity = 30f;

    [Header("Headbob parameters")]
    [SerializeField] private float walkBobSpeed = 14f;
    [SerializeField] private float walkBobAmount = 0.02f;
    [SerializeField] private float sprintBobSpeed = 16f;
    [SerializeField] private float sprintBobAmount = 0.07f;
    private float defaultYPos = 0;
    private float timer;

    // Vars
    private Camera playerCamera;
    private CharacterController characterController;
    private Vector3 moveDirection;
    private Vector2 currentInput;

    [Header("Interaction")]
    [SerializeField] private Vector3 interactionWayPoint = new Vector3(0.5f, 0.5f, 0);
    [SerializeField] private float interactionDistance = 10f;
    [SerializeField] private LayerMask interactionLayer = 7;
    private Interactable currentInteractable;


    private void Awake()
    {
        sprintSpeed = walkSpeed * 2;
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        defaultYPos = playerCamera.transform.localPosition.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            HandleMovementInput();
            if(canJump) HandleJump();
            if (canUseHeadBob) HandleHeadBob();
            ApplyFinalMovements();

            if (canInteract)
            {
                HandleInteractionCheck();
                HandleInteractionInput();
            } 
        }
    }

    private void HandleMovementInput()
    {
        currentInput = new Vector2((IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Vertical"), (IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Horizontal"));
        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
        moveDirection.y = moveDirectionY;
    }
        
    private void HandleJump()
    {
        if (ShouldJump) moveDirection.y = jumpForce;
    }

    private void HandleHeadBob()
    {
        if (!characterController.isGrounded) return;

        // Check player is actually moving
        if (Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
        {
            // Get the timer based on whether player is walking/sprinting/crouching
            timer += Time.deltaTime * (IsSprinting ? sprintBobSpeed : walkBobSpeed);
            // Local pos
            playerCamera.transform.localPosition = new Vector3( // Move Camera up and down
            playerCamera.transform.localPosition.x, // Camera's local position X
            defaultYPos + Mathf.Sin(timer) * (IsSprinting ? sprintBobAmount : walkBobAmount), // change y pos based on state
            playerCamera.transform.localPosition.z); // Camera's local position Y
        }
    }

    private void ApplyFinalMovements()
    {
        if (!characterController.isGrounded) 
            moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void HandleInteractionCheck()
    {
        
        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, 10f))
        {
            //Debug.Log(hit.collider.name);
            if(hit.collider.gameObject.layer == 7 && (currentInteractable == null || hit.collider.gameObject.GetInstanceID() != currentInteractable.GetInstanceID()))
            {
                hit.collider.TryGetComponent<Interactable>(out currentInteractable);

                //Debug.Log("Reached");

                if (currentInteractable)
                {
                    //Debug.Log("after reached");
                    currentInteractable.OnFocus();
                }
                    


            }
        }else if (currentInteractable)
        {
            //Debug.Log("Not hit");
            currentInteractable.OnLoseFocus();
            currentInteractable = null;
        }
    }

    private void HandleInteractionInput()
    {
        if(Input.GetKeyDown(interactKey) && currentInteractable != null && Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, interactionDistance, interactionLayer))
            currentInteractable.OnInteract();
    }
}
