using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Target Frame Rate Setting")]
    [SerializeField] private int targetFrameRate = 60;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = targetFrameRate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
