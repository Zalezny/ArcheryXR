using System;
using System.Collections;
using System.Timers;
using UnityEngine;

public class TargetPointer : MonoBehaviour
{
    public GameObject parentObject;

    public string pointControllerTag = "PointController";

    public TargetCollision targetCollision;

    public string colliderTag = "Arrow";

    public Rigidbody parentRb;

    public Transform centerPoint;
    private Timer _timer;

    private int collectedPoints = 0;
    public void PointAssigner(int points)
    {
        if(points > collectedPoints) {
            targetCollision.GetHit();

            collectedPoints = points;
            GameObject pointController = GameObject.FindGameObjectWithTag(pointControllerTag);

            pointController.GetComponent<PointController>().AddPointsPerRound(points);
            //ExecuteAfterTime(3000, () => Destroy(parentObject));



        }
    }



    private void ExecuteAfterTime(int milliseconds, Action action)
    {
        _timer = new Timer(milliseconds);
        _timer.Elapsed += (sender, e) =>
        {
            _timer.Stop(); // Zatrzymaj Timer po wykonaniu akcji
            _timer.Dispose();
            action?.Invoke();
            Debug.Log("Test Timer invoke");
        };
        _timer.AutoReset = false; // Zapewnia, ¿e Timer uruchomi siê tylko raz
        _timer.Start();
    }

    public void GetHit(Transform arrowHittedTransform)
    {
        Debug.Log("Test GetHit");
        int pointsToCollect = 0;

        var distanceToCenter = Vector3.Distance(arrowHittedTransform.position, centerPoint.position);

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
        PointAssigner(pointsToCollect);



    }
}
