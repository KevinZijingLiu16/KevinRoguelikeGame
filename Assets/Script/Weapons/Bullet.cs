using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    [Header("Elements")]
    private Rigidbody2D rig;

    private Collider2D _collider;
    private RangeWeapon rangeWeapon;

    [Header("Setting")]
    private int damage;
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask enemyMask;
    private Enemy target;
    private bool isCriticalHit;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        //LeanTween.delayedCall(gameObject, 5, () => rangeEnemyAttack.ReleaseBullet(this));
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Shoot (int damage, Vector2 direction, bool isCriticalHit)
    {
        Invoke("Release", 1f);
        this.damage = damage;
        this.isCriticalHit = isCriticalHit;
        transform.right = direction;
    
        rig.linearVelocity = direction * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (target != null) {return;}

       if(IsINLayerMask(collider.gameObject.layer, enemyMask))
        {
            target = collider.GetComponent<Enemy>();
            CancelInvoke();
            Attack(target);

          Release();
          

        }
    }
    private void Attack(Enemy enemy)
    {
        if (enemy != null)
        {
            enemy.TakeDamage(damage, isCriticalHit);
        }
    }
    private bool IsINLayerMask(int layer, LayerMask layerMask)
    {
        return (layerMask.value & (1 << layer)) != 0;
    }
    public void Configure(RangeWeapon rangeWeapon)
    {
        this.rangeWeapon = rangeWeapon;
    }
    public void Reload()
    {
        target = null;
        rig.linearVelocity = Vector2.zero;
        _collider.enabled = true;
        //Release();
       
    }
    private void Release()
    { 
        if(!gameObject.activeSelf)
        {
           return;
        }
        rangeWeapon.ReleaseBullet(this);
      
    }
}
