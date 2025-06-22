using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rig;

    [Header("Joystick Settings")]
    [SerializeField] private MobileJoystick playerJoystick;

    [SerializeField] private float keyboardInputMultiplier = 5f; // Multiplier for keyboard input sensitivity

    private PlayerInputActions inputActions;
    private Vector2 inputVector;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f; // Speed at which the player moves

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        //rig.linearVelocity = Vector2.right ; // Set the initial velocity to the right at 5 units per second
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector2 joystickInput = playerJoystick.GetMoveVector();
        Vector2 inputSystemInput = inputActions.Player.Move.ReadValue<Vector2>();

        bool isUsingJoystick = joystickInput != Vector2.zero;

        Vector2 finalInput = isUsingJoystick
            ? joystickInput * moveSpeed
            : inputSystemInput * moveSpeed * keyboardInputMultiplier;

        rig.linearVelocity = finalInput * moveSpeed * Time.deltaTime;
    }

    private void Update()
    {
        if (Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame)
            Debug.Log("Gamepad is being used");

        if (Keyboard.current != null && Keyboard.current.wasUpdatedThisFrame)
            Debug.Log("Keyboard is being used");
        else
            Debug.Log("No input detected from keyboard or gamepad");
    }
}