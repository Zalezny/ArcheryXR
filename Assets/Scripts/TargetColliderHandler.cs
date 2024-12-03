using UnityEngine;

public class TargetColliderHandler : MonoBehaviour
{

    public int pointsToCollect;

    public string colliderTag = "Arrow";

    public Rigidbody targetRb;

    public TargetController targetController;

    public TargetPointer pointer;

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.collider.CompareTag(colliderTag) && targetRb.isKinematic) 
        {
            targetController.GetHit();
            pointer.PointAssigner(pointsToCollect);
        }
    }

}
