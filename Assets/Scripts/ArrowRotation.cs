using UnityEngine;

/// <summary>
/// Klasa odpowiedzialna za rotacj� strza�y w kierunku jej ruchu.
/// </summary>
public class ArrowRotation : MonoBehaviour
{
    /// <summary>
    /// RigidBody strza�y, u�ywany do uzyskania pr�dko�ci liniowej.
    /// </summary>
    [SerializeField]
    private Rigidbody rb;

    /// <summary>
    /// Ustawia rotacj� strza�y w kierunku jej pr�dko�ci w ka�dej klatce fizyki.
    /// </summary>
    private void FixedUpdate()
    {
        transform.forward = Vector3.Slerp(transform.forward, rb.linearVelocity.normalized, Time.fixedDeltaTime);
    }
}
