using Oculus.Interaction.Grab;
using UnityEngine;

public class TargetCollision : MonoBehaviour
{
    int wallCollisionCount = 0;

    public GameObject blackHole;

    private Vector3 startPos;

    public GameObject visualTarget;

    public Rigidbody rb;
    public MeshCollider parentCollider;

    public TargetController controller;
    public bool isLastTarget;

    public void InitialPosition(Vector3 startPos)
    {
        this.startPos = startPos;
    }

    public void GetHit()
    {
        Debug.Log(" GetHit");
        //parentCollider.isTrigger = false;
        //parentCollider.convex = false;
        rb.isKinematic = false;
        
        controller.DisableMovingAndHapticFeedback();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Test Trigger " + other.gameObject.name);
        if(other.gameObject.name == "WALL_FACE_EffectMesh")
        {
            GameObject generatedHole = Instantiate(blackHole);
            generatedHole.transform.position = transform.position;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector3 collisionNormal = transform.position - startPos;

            collisionNormal.Normalize();
            generatedHole.transform.rotation = Quaternion.Euler(collisionNormal.z * 90, 0, collisionNormal.x * 90);
            visualTarget.GetComponent<MeshRenderer>().enabled = true;


            wallCollisionCount++;
            if (wallCollisionCount > 1)
            {
                controller.RemoveTarget();
            }
        }
       


    }
}
