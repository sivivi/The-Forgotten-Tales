using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    private double gravity = -9.81;
    float yVelocity;

    private float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
 
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
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

        // jump
        if (controller.isGrounded && Input.GetButton("Jump")) {
            yVelocity += Mathf.Sqrt(jumpHeight * -3.0f * (float)gravity);
        }
        
        controller.Move(new Vector3(0, yVelocity, 0) * Time.deltaTime);
    }
}
