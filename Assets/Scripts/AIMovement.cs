using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    Animator animator;

    public float moveSpeed = 0.2f;

    Vector3 stopPosition;

    float walkTime;
    float walkCounter;
    float waitTime;
    float waitCounter;

    int WalkDirection;

    public bool isWalking;

    void Start()
    {
        animator = GetComponent<Animator>();

        // So that all the prefabs don't move/stop at the same time
        walkTime = Random.Range(10, 15);
        waitTime = Random.Range(5, 7);


        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();
    }

    void Update()
    {
        if (isWalking)
        {

            animator.SetBool("isRunning", true);

            walkCounter -= Time.deltaTime;


            // Set movement direction
            switch (WalkDirection)
            {
                case 0:
                    transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
                case 1:
                    transform.localRotation = Quaternion.Euler(0f, 90, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
                case 2:
                    transform.localRotation = Quaternion.Euler(0f, -90, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
                case 3:
                    transform.localRotation = Quaternion.Euler(0f, 180, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
            }

            if (walkCounter <= 0)
            {
                stopPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                isWalking = false;
                // Stop movement
                transform.position = stopPosition;
                animator.SetBool("isRunning", false);
                // Reset the waitCounter
                waitCounter = waitTime;
            }


        }
        else
        {

            waitCounter -= Time.deltaTime;

            if (waitCounter <= 0)
            {
                ChooseDirection();
            }
        }
    }


    public void ChooseDirection()
    {
        WalkDirection = Random.Range(0, 4);

        isWalking = true;
        walkCounter = walkTime;
    }
}
