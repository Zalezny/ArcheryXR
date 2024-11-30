using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Sprawdzanie, czy animacja si� zako�czy�a
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 &&
            !animator.IsInTransition(0))
        {
            Destroy(gameObject); // Usu� obiekt
        }
    }
}