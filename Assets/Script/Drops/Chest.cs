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

        animator.SetBool("IsOpen", true);

        if (openEffect != null)
        {
            var force = openEffect.forceOverLifetime;
            force.enabled = true;

            Vector3 dir = (player.transform.position - openEffect.transform.position).normalized;

            force.x = new ParticleSystem.MinMaxCurve(dir.x * 1f);
            force.y = new ParticleSystem.MinMaxCurve(dir.y * 1f);
            

            openEffect.Play();
        }

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
