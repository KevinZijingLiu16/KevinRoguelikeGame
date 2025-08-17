using System.Collections;
using UnityEngine;

public abstract class DroppableCurreny : MonoBehaviour, ICollectable
{
    private bool collected;
    [Header("Candy Settings")]
    [SerializeField] protected float moveSpeed = 3f; // Speed at which the candy moves towards the player
                                                     // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        collected = false;
    
    }

    public void Collect(Player player)
    {
        if (collected)
            return;
        collected = true;

        StartCoroutine(MoveTowardsPlayer(player));


    }

    IEnumerator MoveTowardsPlayer(Player player)
    {
        float timer = 0;

        Vector2 initialPosition = transform.position;
        //Vector2 targetPosition = player.GetPlayerCenter();
        while (timer < 1)
        {
            Vector2 targetPosition = player.GetPlayerCenter();
            transform.position = Vector2.Lerp(initialPosition, targetPosition, timer);
            timer += Time.deltaTime * moveSpeed;
            yield return null;
        }
        Collectted();
    }
    protected abstract void Collectted();
    
}
