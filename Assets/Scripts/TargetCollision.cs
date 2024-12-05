using Oculus.Interaction.Grab;
using UnityEngine;

public class TargetCollision : MonoBehaviour
{
    int wallCollisionCount = 0;

    public GameObject blackHole;

    private Vector3 startPos;

    public GameObject visualTarget;

    public Rigidbody rb;

    public TargetController controller;

    public void InitialPosition(Vector3 startPos)
    {
        this.startPos = startPos;
    }

    public void GetHit()
    {
        UnityEngine.Debug.Log(" GetHit");

        rb.isKinematic = false;
        controller.DisableMovingAndHapticFeedback();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "WALL_FACE_EffectMesh")
        {
            GameObject generatedHole = Instantiate(blackHole);
            generatedHole.transform.position = transform.position;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            //Vector3 collisionNormal = other.ClosestPointOnBounds(transform.position) - transform.position;
            Vector3 collisionNormal = transform.position - startPos;

            collisionNormal.Normalize();
            generatedHole.transform.rotation = Quaternion.Euler(collisionNormal.z * 90, 0, collisionNormal.x * 90);
            visualTarget.GetComponent<MeshRenderer>().enabled = true;


            wallCollisionCount++;
            if (wallCollisionCount > 1)
            {
                //isMoving = false; // Zatrzymaj ruch po osi¹gniêciu celu
                Destroy(gameObject);

                
            }
        }
       


    }
}
