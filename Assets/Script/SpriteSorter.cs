using UnityEngine;

public class SpriteSorter : MonoBehaviour
{
    [Header("Sprite Sorting Settings")]
    [SerializeField] private SpriteRenderer spriteRender;

    private void Update()
    {
        spriteRender.sortingOrder = -(int)(transform.position.y * 100f);
    }
}
