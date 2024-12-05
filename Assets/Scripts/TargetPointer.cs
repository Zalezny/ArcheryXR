using System.Collections;
using UnityEngine;

public class TargetPointer : MonoBehaviour
{
    public GameObject parentObject;

    public string pointControllerTag = "PointController";

    public TargetCollision targetCollision;

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
        yield return new WaitForSeconds(time); // Wstrzymaj wykonanie na okre�lony czas
        Debug.Log("Wykonano po " + time + " sekundach"); // Kod do wykonania po up�ywie czasu
    }
}
