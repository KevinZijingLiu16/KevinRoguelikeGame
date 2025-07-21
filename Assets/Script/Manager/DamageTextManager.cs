using UnityEngine;
using UnityEngine.Pool;

public class DamageTextManager : MonoBehaviour
{
    [Header("Element References")]
    [SerializeField] private DamageTextEffect damageTextEffectPrefab;

    [Header("Pooling")]
    private ObjectPool<DamageTextEffect> damageTextEffectPool;

    private void Awake()
    {
        MeleeEnemy.onDamageTaken += EnemyHitCallback;
    }

    private void OnDestroy()
    {
        MeleeEnemy.onDamageTaken -= EnemyHitCallback;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        damageTextEffectPool = new ObjectPool<DamageTextEffect>(CreateFunction, ActionOnGet, ActionOnRelease, ActionOnDestroy);
    }

    private DamageTextEffect CreateFunction()
    {
        return Instantiate(damageTextEffectPrefab, transform);
    }

    private void ActionOnGet(DamageTextEffect damageTextEffect)
    {
        damageTextEffect.gameObject.SetActive(true);
    }

    private void ActionOnRelease(DamageTextEffect damageTextEffect)
    {
        damageTextEffect.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(DamageTextEffect damageTextEffect)
    {
        Destroy(damageTextEffect.gameObject);
    }

    private void EnemyHitCallback(int damage, Vector2 enemyPosition)
    {
        DamageTextEffect damageTextInstance = damageTextEffectPool.Get();
        Vector3 spawnPosition = enemyPosition + Vector2.up * 1.5f;
        damageTextInstance.transform.position = spawnPosition;

        damageTextInstance.Animate(damage);

        LeanTween.delayedCall(1, () => damageTextEffectPool.Release(damageTextInstance));
    }
}