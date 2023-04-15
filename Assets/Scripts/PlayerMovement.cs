using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public Vector3 playerVelocity;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public double gravity = -9.81;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        controller.skinWidth = 0.0001f;
    }
 
    // Update is called once per frame
    void Update()
    {
        // gravity
        playerVelocity.y += (float) gravity * Time.deltaTime;

        if (controller.isGrounded && playerVelocity.y < 0) {
            playerVelocity.y = 0f;
        }

        // jump
        if (controller.isGrounded && Input.GetButton("Jump")) {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * (float)gravity);
        }

        // walk
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        controller.Move(move.normalized * Time.deltaTime * playerSpeed);
        
        if (move != Vector3.zero) {
            gameObject.transform.forward = move;
        }

        controller.Move(playerVelocity * Time.deltaTime);
    }
}
