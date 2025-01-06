using UnityEngine;

/// <summary>
/// Zarz¹dza zachowaniem strza³y, która po kontakcie przyczepia siê do powierzchni lub obiektu.
/// </summary>
public class StickingArrow : MonoBehaviour
{
    /// <summary>
    /// Komponent Rigidbody u¿ywany do kontroli fizyki strza³y.
    /// </summary>
    [SerializeField]
    private Rigidbody rb;

    /// <summary>
    /// Kolider kuli u¿ywany do obs³ugi interakcji po przyczepieniu strza³y.
    /// </summary>
    [SerializeField]
    private SphereCollider arrowPickCollider;

    /// <summary>
    /// Prefab strza³y, która bêdzie wizualnie przyczepiana do powierzchni po trafieniu.
    /// </summary>
    [SerializeField]
    private GameObject stickingArrow;

    /// <summary>
    /// Wywo³ywana, gdy strza³a wchodzi w obszar triggera.
    /// Tworzy przyczepion¹ strza³ê, ustawia jej pozycjê i orientacjê,
    /// a nastêpnie usuwa aktualny obiekt strza³y.
    /// </summary>
    /// <param name="other">Kolider, z którym strza³a wesz³a w interakcjê.</param>
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
    /// Wywo³ywana, gdy strza³a zderza siê z innym obiektem.
    /// Tworzy przyczepion¹ strza³ê, ustawia jej pozycjê i orientacjê,
    /// a nastêpnie usuwa aktualny obiekt strza³y.
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
