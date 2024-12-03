using System.Collections;
using UnityEngine;

public class TargetPointer : MonoBehaviour
{
    public GameObject parentObject;

    private int collectedPoints = 0;
    public void PointAssigner(int points)
    {
        if(points > collectedPoints) { 
            collectedPoints = points;
            StartCoroutine(ExecuteAfterTime(1f));
            // TODO: to pointcontroller
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
