using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class EnemyBullet : MonoBehaviour
{
    [Header("Elements")]
    private Rigidbody2D rig;

    private Collider2D _collider;
    private RangeEnemyAttack rangeEnemyAttack;

    [Header("Setting")]
    private int damage;

    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        //StartCoroutine(ReleaseCoroutine());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            LeanTween.cancel(gameObject);

            //StopAllCoroutines();
            player.TakeDamage(damage);
            this._collider.enabled = false;

            rangeEnemyAttack.ReleaseBullet(this);
        }
    }

    public void Shoot(int damage, Vector2 direction)
    {
        this.damage = damage;
        transform.right = direction;
        rig.linearVelocity = direction * moveSpeed;
    }

    public void Configure(RangeEnemyAttack rangeEnemyAttack)
    {
        this.rangeEnemyAttack = rangeEnemyAttack;
    }

    public void Reload()
    {
        rig.linearVelocity = Vector2.zero;
        _collider.enabled = true;
        LeanTween.delayedCall(gameObject, 5, () => rangeEnemyAttack.ReleaseBullet(this));
    }

    private IEnumerator ReleaseCoroutine()
    {
        yield return new WaitForSeconds(5);
        rangeEnemyAttack.ReleaseBullet(this);
    }
}