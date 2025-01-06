using UnityEngine;

/// <summary>
/// Zarz�dza zachowaniem strza�y, kt�ra po kontakcie przyczepia si� do powierzchni lub obiektu.
/// </summary>
public class StickingArrow : MonoBehaviour
{
    /// <summary>
    /// Komponent Rigidbody u�ywany do kontroli fizyki strza�y.
    /// </summary>
    [SerializeField]
    private Rigidbody rb;

    /// <summary>
    /// Kolider kuli u�ywany do obs�ugi interakcji po przyczepieniu strza�y.
    /// </summary>
    [SerializeField]
    private SphereCollider arrowPickCollider;

    /// <summary>
    /// Prefab strza�y, kt�ra b�dzie wizualnie przyczepiana do powierzchni po trafieniu.
    /// </summary>
    [SerializeField]
    private GameObject stickingArrow;

    /// <summary>
    /// Wywo�ywana, gdy strza�a wchodzi w obszar triggera.
    /// Tworzy przyczepion� strza��, ustawia jej pozycj� i orientacj�,
    /// a nast�pnie usuwa aktualny obiekt strza�y.
    /// </summary>
    /// <param name="other">Kolider, z kt�rym strza�a wesz�a w interakcj�.</param>
    private void OnTriggerEnter(Collider other)
    {
        rb.isKinematic = true;
        arrowPickCollider.isTrigger = true;

        GameObject stickingArrowGO = Instantiate(stickingArrow);
        stickingArrowGO.transform.position = transform.position;
        stickingArrowGO.transform.forward = transform.forward;

        if (other.attachedRigidbody != null)
        {
            stickingArrowGO.transform.parent = other.attachedRigidbody.transform;
        }

        Destroy(gameObject);
    }

    /// <summary>
    /// Wywo�ywana, gdy strza�a zderza si� z innym obiektem.
    /// Tworzy przyczepion� strza��, ustawia jej pozycj� i orientacj�,
    /// a nast�pnie usuwa aktualny obiekt strza�y.
    /// </summary>
    /// <param name="collision">Informacje o kolizji.</param>
    private void OnCollisionEnter(Collision collision)
    {
        rb.isKinematic = true;
        arrowPickCollider.isTrigger = true;

        GameObject stickingArrowGO = Instantiate(stickingArrow);
        stickingArrowGO.transform.position = transform.position;
        stickingArrowGO.transform.forward = transform.forward;

        if(collision.collider.attachedRigidbody != null)
        {
            stickingArrowGO.transform.parent = collision.collider.attachedRigidbody.transform;
            collision.collider.GetComponent<TargetPointer>()?.GetHit(stickingArrowGO.transform.parent);

        }

        Destroy(gameObject);
    }
}
