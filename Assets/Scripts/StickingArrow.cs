using Oculus.Interaction;
using UnityEngine;

public class StickingArrow : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private SphereCollider myCollider;
    [SerializeField]
    private GameObject stickingArrow;

    private void OnTriggerEnter(Collider other)
    {
        rb.isKinematic = true;
        myCollider.isTrigger = true;

        GameObject arrow = Instantiate(stickingArrow);
        arrow.transform.position = transform.position;
        arrow.transform.forward = transform.forward;

        if (other.attachedRigidbody != null)
        {
            arrow.transform.parent = other.attachedRigidbody.transform;
        }

        //other.GetComponent<TargetController>()?.GetHit();

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.isKinematic = true;
        myCollider.isTrigger = true;

        GameObject arrow = Instantiate(stickingArrow);
        arrow.transform.position = transform.position;
        arrow.transform.forward = transform.forward;

        if(collision.collider.attachedRigidbody != null)
        {
            arrow.transform.parent = collision.collider.attachedRigidbody.transform;

        }

        collision.collider.GetComponent<TargetController>()?.GetHit();

        Destroy(gameObject);
    }
}
