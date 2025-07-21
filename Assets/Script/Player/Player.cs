using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour
{
    [Header("Component")]
    private PlayerHealth playerHealth;

    [SerializeField] private CircleCollider2D collider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void TakeDamage(int damage)
    {
        playerHealth.TakeDamage(damage);
    }

    public Vector2 GetPlayerCenter()
    {
        return (Vector2)transform.position + collider.offset;
    }
}