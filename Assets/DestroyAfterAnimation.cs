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
        // Sprawdzanie, czy animacja siê zakoñczy³a
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 &&
            !animator.IsInTransition(0))
        {
            Destroy(gameObject); // Usuñ obiekt
        }
    }
}