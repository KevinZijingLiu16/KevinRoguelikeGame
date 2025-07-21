using TMPro;
using UnityEngine;

public class DamageTextEffect : MonoBehaviour
{
    [Header("Element References")]
    [SerializeField] private Animator animator;

    [SerializeField] private TextMeshPro damageText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    [NaughtyAttributes.Button]
    public void Animate(int damage)
    {
        damageText.text = damage.ToString();
        animator.Play("DamageTextAnimation");
    }
}