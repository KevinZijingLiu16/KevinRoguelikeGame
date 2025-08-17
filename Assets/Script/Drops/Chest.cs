using UnityEngine;
using System;
using System.Collections;

public class Chest : MonoBehaviour, ICollectable
{
    [Header("Action")]
    public static Action onCollected;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("Effects")]
    [SerializeField] private ParticleSystem openEffect;


    public void Collect(Player player)
    {
      onCollected?.Invoke();
        // Play collect animation
        animator.SetBool("IsOpen", true);

        if (openEffect != null)
        {
            openEffect.Play();
        }
        // Play collect sound
        // Add rewards to player inventory
        StartCoroutine(DestroyAfterAnimation());
    }

    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(1.0f); // Replace 1.0f with actual "Open" animation length
        if (openEffect != null)
            yield return new WaitForSeconds(openEffect.main.duration);

        Destroy(gameObject);
    }

}
