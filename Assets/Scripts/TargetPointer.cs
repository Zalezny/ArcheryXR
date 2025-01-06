using System;
using System.Timers;
using UnityEngine;

/// <summary>
/// Zarz�dza logik� trafie� w cel, przypisywaniem punkt�w oraz odleg�o�ci� trafienia od �rodka celu.
/// </summary>
public class TargetPointer : MonoBehaviour
{
    /// <summary>
    /// Obiekt nadrz�dny, do kt�rego nale�y cel.
    /// </summary>
    public GameObject parentObject;

    /// <summary>
    /// Tag kontrolera punkt�w u�ywanego do zarz�dzania wynikami.
    /// </summary>
    public string pointControllerTag = "PointController";

    /// <summary>
    /// Obiekt obs�uguj�cy logik� kolizji celu.
    /// </summary>
    public TargetCollision targetCollision;

    /// <summary>
    /// Tag u�ywany do identyfikacji strza�y koliduj�cej z celem.
    /// </summary>
    public string colliderTag = "Arrow";

    /// <summary>
    /// Komponent Rigidbody obiektu nadrz�dnego.
    /// </summary>
    public Rigidbody parentRb;

    /// <summary>
    /// Transform okre�laj�cy �rodek celu.
    /// </summary>
    public Transform centerPoint;

    /// <summary>
    /// Timer u�ywany do op�nionego wykonania akcji.
    /// </summary>
    private Timer _timer;

    /// <summary>
    /// Liczba punkt�w zebranych przez gracza dla tego celu.
    /// </summary>
    private int collectedPoints = 0;

    /// <summary>
    /// Przypisuje punkty do kontrolera punkt�w, je�li warto�� punkt�w jest wi�ksza od obecnych.
    /// </summary>
    /// <param name="points">Liczba punkt�w do przypisania.</param>
    public void PointAssigner(int points)
    {
        if(points > collectedPoints) {
            targetCollision.GetHit();

            collectedPoints = points;
            GameObject pointController = GameObject.FindGameObjectWithTag(pointControllerTag);

            pointController.GetComponent<PointController>().AddPointsPerRound(points);
        }
    }

    /// <summary>
    /// Obs�uguje trafienie celu przez strza�� i przypisuje odpowiedni� liczb� punkt�w w zale�no�ci od odleg�o�ci od �rodka celu.
    /// </summary>
    /// <param name="arrowHittedTransform">Transform strza�y, kt�ra trafi�a w cel.</param>
    public void GetHit(Transform arrowHittedTransform)
    {
        int pointsToCollect;

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
