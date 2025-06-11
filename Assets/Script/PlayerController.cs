using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rig;
    [Header("Joystick Settings")]
    [SerializeField] private MobileJoystick playerJoystick;
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f; // Speed at which the player moves

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       rig = GetComponent<Rigidbody2D>();
       rig.linearVelocity = Vector2.right ; // Set the initial velocity to the right at 5 units per second
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rig.linearVelocity = playerJoystick.GetMoveVector()*moveSpeed*Time.deltaTime; // Set the velocity based on joystick input, scaled by 5 units per second
    }
}
