using UnityEngine;

public class CameraControl2D : MonoBehaviour
{
    [Header("Camera Control Settings")]
    [SerializeField] private Transform target;
    [SerializeField] private float cameraZOffset = -10f; // Default camera Z offset for 2D view
    //[SerializeField] private float minimumX;
    //[SerializeField] private float maximumX;
    //[SerializeField] private float minimumY;
    //[SerializeField] private float maximumY;
    [SerializeField] private Vector2 minMaxXY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        if (target == null)
        { 
         Debug.LogWarning("Target is not assigned in CameraControl2D. Please assign a target Transform.");
            return;
        }
        Vector3 targetPosition = target.position;
        targetPosition.z = cameraZOffset;

        // Clamp the camera position within the specified bounds
        targetPosition.x = Mathf.Clamp(targetPosition.x, -minMaxXY.x, minMaxXY.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -minMaxXY.y, minMaxXY.y);
        transform.position = targetPosition;
    }

}
