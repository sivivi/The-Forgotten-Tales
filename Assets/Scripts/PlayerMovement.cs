using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Vector3 playerVelocity;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public bool isGrounded = true;
    public double gravity = -9.81;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }
 
    // Update is called once per frame
    void Update()
    {
        // jump
        if (isGrounded && Input.GetButtonDown("Jump")) {
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

    void OnCollisionStay(Collision collisionInfo)
    {
        // Debug-draw all contact points and normals
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal * 10, Color.blue);
            Debug.Log($"a");
        }
    }
}
