using UnityEngine;

[RequireComponent(typeof(PlayerHealth),typeof(PlayerLevel))]
public class Player : MonoBehaviour
{
    public static Player instance;
    [Header("Component")]
    private PlayerHealth playerHealth;
    private PlayerLevel playerLevel;

    [SerializeField] private CircleCollider2D collider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
           // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        playerHealth = GetComponent<PlayerHealth>();
        playerLevel = GetComponent<PlayerLevel>();
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

    public bool HasLeveledUp()
    {
        return playerLevel.HasLeveledUp();
    }
}