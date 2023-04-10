using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private Rigidbody rb;
    private Collider groundCheck;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        rb = gameObject.AddComponent<Rigidbody>();
        groundCheck = gameObject.AddComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        // jump
        if (groundCheck.isTrigger && Input.GetButtonDown("Jump")) {
            playerVelocity.y += jumpHeight;
        }

        // walk
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        controller.Move(playerVelocity);
    }
}
