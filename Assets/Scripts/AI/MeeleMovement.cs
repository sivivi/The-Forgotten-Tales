using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MeeleMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 2.0f;
    public float jumpHeight = 1.0f;
    private double gravity = -9.81;
    float yVelocity;

    public float turnSmoothTime = 120f;
    public GameObject target;
    public bool targetFriendlys;
    public float targetRange;
    private UnityEngine.AI.NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.speed = speed;
        agent.angularSpeed = turnSmoothTime;
    }

    // Update is called once per frame
    void Update()
    {
        // get new target
        if (target == null || Vector3.Distance(transform.position, target.transform.position) > targetRange){
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Player").Concat(targetFriendlys ? GameObject.FindGameObjectsWithTag("Enemy") : null).ToArray();
            Debug.Log(targets);

            GameObject closestTarget = null;
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