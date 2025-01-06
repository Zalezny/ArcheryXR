using UnityEngine;

/// <summary>
/// Klasa odpowiedzialna za rotacjê strza³y w kierunku jej ruchu.
/// </summary>
public class ArrowRotation : MonoBehaviour
{
    /// <summary>
    /// RigidBody strza³y, u¿ywany do uzyskania prêdkoœci liniowej.
    /// </summary>
    [SerializeField]
    private Rigidbody rb;

    /// <summary>
    /// Ustawia rotacjê strza³y w kierunku jej prêdkoœci w ka¿dej klatce fizyki.
    /// </summary>
    private void FixedUpdate()
    {
        transform.forward = Vector3.Slerp(transform.forward, rb.linearVelocity.normalized, Time.fixedDeltaTime);
    }
}
