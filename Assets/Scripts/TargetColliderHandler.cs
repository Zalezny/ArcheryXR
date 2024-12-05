using UnityEngine;

public class TargetColliderHandler : MonoBehaviour
{

    public int pointsToCollect;

    public string colliderTag = "Arrow";

    public Rigidbody targetRb;

    public TargetPointer pointer;

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.collider.CompareTag(colliderTag) && targetRb.isKinematic) 
        {
            pointer.PointAssigner(pointsToCollect);
        }
    }

}
