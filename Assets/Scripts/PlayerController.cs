using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    CharacterController characterController;
    float moveSpeed = 5;
    int lastDirection;

    public string[] staticDirections = { "Static N", "Static NW", "Static W", "Static SW", "Static S", "Static SE", "Static E", "Static NE" };
    public string[] runDirections = { "Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE" };

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        string[] directionArray;
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (movement.magnitude > 0.01f)
        {
            characterController.Move(movement * moveSpeed * Time.fixedDeltaTime);
            directionArray = runDirections;
            lastDirection = DirectionToIndex(movement);
        }
        else
        {
            directionArray = staticDirections;
        }

        animator.Play(directionArray[lastDirection]);
    }

    private int DirectionToIndex(Vector3 movement)
    {
        float step = 360 / 8;
        float offset = step / 2;
        float angle = Vector2.SignedAngle(Vector2.up, movement.normalized);
        angle += offset;

        if (angle < 0)
            angle += 360;

        return Mathf.FloorToInt(angle / step);
    }
}
