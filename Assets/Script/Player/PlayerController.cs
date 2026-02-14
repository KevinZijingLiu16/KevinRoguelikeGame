using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IPlayerStatsDependency
{
    private Rigidbody2D rig;

    [Header("Joystick Settings")]
    [SerializeField] private MobileJoystick playerJoystick;

  

   
    private Vector2 inputVector;

    [Header("Movement Settings")]
    [SerializeField] private float baseMoveSpeed = 5f; // Base speed at which the player moves
     private float moveSpeed = 5f; // Speed at which the player moves

  
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        //rig.linearVelocity = Vector2.right ; // Set the initial velocity to the right at 5 units per second
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
       

        rig.linearVelocity = playerJoystick.GetMoveVector()* moveSpeed * Time.deltaTime;
    }

    private void Update()
    {
       
    }

    public void UpdateStats(PlayerStatsManager playerStatsManager)
    {
        float _moveSpeedPercent = playerStatsManager.GetStatValue(Stat.MoveSpeed) / 100f;
        moveSpeed = baseMoveSpeed *(1 + _moveSpeedPercent);
    }
}