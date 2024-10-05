using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : Enitities
{
    CharacterController controller;
    Animator animator;
    public float gravity = 9.8f;
    public float sprintAdittion = 3.5f;
    float jumpElapsedTime = 0;

    // Player states
    bool isJumping = false;
    bool isSprinting = false;
    bool isCrouching = false;

    // Inputs
    float inputHorizontal;
    float inputVertical;
    bool inputJump;
    bool inputCrouch;
    bool inputSprint;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // Message informing the user that they forgot to add an animator
        if (animator == null)
            Debug.LogWarning("Hey buddy, you don't have the Animator component in your player. Without it, the animations won't work.");
    }

    // Update is called once per frame
    void Update()
    {
        // Input checkers
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        inputJump = Input.GetAxis("Jump") == 1f;
        inputSprint = Input.GetAxis("Fire3") == 1f;
        // Unfortunately GetAxis does not work with GetKeyDown, so inputs must be taken individually
        inputCrouch = Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.JoystickButton1);

        // Check if you pressed the crouch input key and change the player's state
        if ( inputCrouch )
            isCrouching = !isCrouching;

        // Run and Crouch animation
        // If dont have animator component, this block wont run
        if ( controller.isGrounded && animator != null )
        {

            // Crouch
            // Note: The crouch animation does not shrink the character's collider
            animator.SetBool("crouch", isCrouching);
            
            // Run
            float minimumSpeed = 0.9f;
            animator.SetBool("run", controller.velocity.magnitude > minimumSpeed );

            // Sprint
            isSprinting = controller.velocity.magnitude > minimumSpeed && inputSprint;
            animator.SetBool("sprint", isSprinting );

        }

        // Jump animation
        if( animator != null )
            animator.SetBool("air", controller.isGrounded == false );

        // Handle can jump or not
        if ( inputJump && controller.isGrounded )
        {
            isJumping = true;
            // Disable crounching when jumping
            isCrouching = false; 
        }
        HeadHittingDetect();

        
    }

    void FixedUpdate()
    {
        Move();
        Atack();
    }
    void HeadHittingDetect()
    {
        float headHitDistance = 1.1f;
        Vector3 controllerCenter = transform.TransformPoint(controller.center);
        float hitCalc = controller.height / 2f * headHitDistance;

        // Uncomment this line to see the Ray drawed in your characters head
        // Debug.DrawRay(controllerCenter, Vector3.up * headHeight, Color.red);

        if (Physics.Raycast(controllerCenter, Vector3.up, hitCalc))
        {
            jumpElapsedTime = 0;
            isJumping = false;
        }
    }

    protected override void Move()
    {
        // Sprinting velocity boost or crounching desacelerate
        float velocityAdittion = 0;
        if ( isSprinting )
            velocityAdittion = sprintAdittion;
        if (isCrouching)
            velocityAdittion =  - (speed * 0.50f); // -50% velocity

        // Direction movement
        float directionX = inputHorizontal * (speed + velocityAdittion) * Time.deltaTime;
        float directionZ = inputVertical * (speed + velocityAdittion) * Time.deltaTime;
        float directionY = 0;

        // Jump handler
        if ( isJumping )
        {
            // Apply inertia and smoothness when climbing the jump
            // It is not necessary when descending, as gravity itself will gradually pulls
            directionY = Mathf.SmoothStep(jumpForce, jumpForce * 0.30f, jumpElapsedTime / jumpTime) * Time.deltaTime;

            // Jump timer
            jumpElapsedTime += Time.deltaTime;
            if (jumpElapsedTime >= jumpTime)
            {
                isJumping = false;
                jumpElapsedTime = 0;
            }
        }

        // Add gravity to Y axis
        directionY = directionY - gravity * Time.deltaTime;

        
        // --- Character rotation --- 
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // Relate the front with the Z direction (depth) and right with X (lateral movement)
        forward = forward * directionZ;
        right = right * directionX;

        if (directionX != 0 || directionZ != 0)
        {
            float angle = Mathf.Atan2(forward.x + right.x, forward.z + right.z) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
        }

        // --- End rotation ---

        
        Vector3 verticalDirection = Vector3.up * directionY;
        Vector3 horizontalDirection = forward + right;

        Vector3 moviment = verticalDirection + horizontalDirection;
        controller.Move( moviment );
    }

    protected override void Atack()
    {
        if (Input.GetMouseButtonDown(0) && Cursor.visible == false)
        {
            animator.SetTrigger("attack");
        }
    }
}
