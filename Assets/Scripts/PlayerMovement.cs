using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private InputMaster controls;
    public CharacterController controller;
    public Transform cam;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    private double gravity = -9.81;
    [SerializeField] private float yVelocity;

    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
 
    private void Awake()
    {
        controls = new InputMaster();
        controls.Player.Jump.performed += ctx => Jump();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = controls.Player.Movement.ReadValue<Vector2>();

        if (direction.magnitude >= 0.1f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * playerSpeed * Time.deltaTime);
        }
        
        // gravity
        yVelocity += (float) gravity * Time.deltaTime;

        if (controller.isGrounded && yVelocity < 0) {
            yVelocity = 0f;
        }

        controller.Move(new Vector3(0, yVelocity, 0) * Time.deltaTime);
    }

    void Jump() {
        Debug.Log("jump");
        if (controller.isGrounded) {
            yVelocity += Mathf.Sqrt(jumpHeight * -3.0f * (float)gravity);
        }
    }

    void Move(Vector2 input) {
        Debug.Log(input);
    }

    private void OnEnable() {
        controls.Enable();
    }

    private void OnDisable() {
        controls.Disable();
    }
}
