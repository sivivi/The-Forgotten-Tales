using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 2.0f;
    public float jumpHeight = 1.0f;
    private double gravity = -9.81;
    float yVelocity;

    private float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Transform target;
    public bool targetFriendlys;
    public float targetRange;

    // Update is called once per frame
    void Update()
    {
        // get new target
        if (target == null || transform.position > targetRange){
            target = GameObject.FindGameObjectWithTag("Player") + (targetFriendlys ? GameObject.FindGameObjectsWithTag("Enemy") : null);

            closestTarget = null;
            float closestDistance = targetRange;
            foreach (GameObject i in targets){
                float distance = Vector3.Distance(transform.position, i.transform.position);
                if (distance < closestDistance){
                    closestDistance = distance;
                    closestTarget = i;
                }
            }
            
            target = closestTarget;
        }

        // gravity
        yVelocity += (float) gravity * Time.deltaTime;

        if (controller.isGrounded && yVelocity < 0) {
            yVelocity = 0f;
        }
    }
}
