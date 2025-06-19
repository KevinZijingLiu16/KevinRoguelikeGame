using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Player player;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float playerDetectionRadius;
    [SerializeField] private ParticleSystem passAwayParticles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<Player>();
        if (player == null)
        {
            Debug.Log("No player found.");
            Destroy(gameObject);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        TryAttack();
    }

    private void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;

        Vector2 targetPostion = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;

        transform.position = targetPostion;
    }
    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
        if (distanceToPlayer <= playerDetectionRadius)
        {
            PassAway();
           
            Debug.Log("Enemy attacked the player!");
        }
    }
    private void PassAway()
    {
        passAwayParticles.transform.SetParent(null);
        // or passAwayParticles.transform.parent == null;
        passAwayParticles.Play();
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }
}
