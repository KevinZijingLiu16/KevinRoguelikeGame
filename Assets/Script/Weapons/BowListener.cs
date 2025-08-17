using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BowListener : MonoBehaviour
{
    [Header("Elements")]
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    private void OnEnable()
    {
        Bullet.OnBulletShot += HandleShot;
        Bullet.OnBulletReleased += HandleReleased;
    }

    private void OnDisable()
    {
        Bullet.OnBulletShot -= HandleShot;
        Bullet.OnBulletReleased -= HandleReleased;
    }

    private void HandleShot(Bullet bullet)
    {
        animator.enabled = true;
    }

    private void HandleReleased(Bullet bullet)
    {
      
        animator.enabled = false;


    }
}
