using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class TargetPointer : MonoBehaviour
{
    public GameObject parentObject;

    public string pointControllerTag = "PointController";

    private int collectedPoints = 0;
    public void PointAssigner(int points)
    {
        if(points > collectedPoints) { 
            collectedPoints = points;
            StartCoroutine(ExecuteAfterTime(1f));
            GameObject pointController = GameObject.FindGameObjectWithTag(pointControllerTag);
            if (pointController == null) { Assert.Fail("Point Controller nie istnieje"); }
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
}
