using System.Collections;
using UnityEngine;

public class TargetPointer : MonoBehaviour
{
    public GameObject parentObject;

    public string pointControllerTag = "PointController";

    public TargetCollision targetCollision;

    public string colliderTag = "Arrow";

    public Rigidbody parentRb;

    public Transform centerPoint;

    private int collectedPoints = 0;
    public void PointAssigner(int points)
    {
        if(points > collectedPoints) { 
            targetCollision.GetHit();

            collectedPoints = points;
            StartCoroutine(ExecuteAfterTime(1f));
            GameObject pointController = GameObject.FindGameObjectWithTag(pointControllerTag);
            pointController.GetComponent<PointController>().addPoints(points);
            StartCoroutine(ExecuteAfterTime(3f));
            Destroy(parentObject);

        }
    }



    private IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time); // Wstrzymaj wykonanie na okreœlony czas
        Debug.Log("Wykonano po " + time + " sekundach"); // Kod do wykonania po up³ywie czasu
    }

    private void OnCollisionEnter(Collision collision)
    {
        int pointsToCollect = 0;
        var colliderPosition = collision.collider.gameObject.transform.position;
        var distanceToCenter = Vector3.Distance(colliderPosition, centerPoint.position);

        if (distanceToCenter <= 0.04)
        {
            pointsToCollect = 100;
        }else if( distanceToCenter <= 0.14)
        {
            pointsToCollect = 75;
        } else if( distanceToCenter <= 0.24)
        {
            pointsToCollect = 50;
        }
        else
        {
            pointsToCollect = 25;
        }


        if (collision.gameObject. CompareTag(colliderTag) || collision.collider.CompareTag(colliderTag))
        {
           PointAssigner(pointsToCollect);
        }
    }
}
