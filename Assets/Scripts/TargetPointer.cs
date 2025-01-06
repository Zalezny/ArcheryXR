using System;
using System.Timers;
using UnityEngine;

/// <summary>
/// Zarz¹dza logik¹ trafieñ w cel, przypisywaniem punktów oraz odleg³oœci¹ trafienia od œrodka celu.
/// </summary>
public class TargetPointer : MonoBehaviour
{
    /// <summary>
    /// Obiekt nadrzêdny, do którego nale¿y cel.
    /// </summary>
    public GameObject parentObject;

    /// <summary>
    /// Tag kontrolera punktów u¿ywanego do zarz¹dzania wynikami.
    /// </summary>
    public string pointControllerTag = "PointController";

    /// <summary>
    /// Obiekt obs³uguj¹cy logikê kolizji celu.
    /// </summary>
    public TargetCollision targetCollision;

    /// <summary>
    /// Tag u¿ywany do identyfikacji strza³y koliduj¹cej z celem.
    /// </summary>
    public string colliderTag = "Arrow";

    /// <summary>
    /// Komponent Rigidbody obiektu nadrzêdnego.
    /// </summary>
    public Rigidbody parentRb;

    /// <summary>
    /// Transform okreœlaj¹cy œrodek celu.
    /// </summary>
    public Transform centerPoint;

    /// <summary>
    /// Timer u¿ywany do opóŸnionego wykonania akcji.
    /// </summary>
    private Timer _timer;

    /// <summary>
    /// Liczba punktów zebranych przez gracza dla tego celu.
    /// </summary>
    private int collectedPoints = 0;

    /// <summary>
    /// Przypisuje punkty do kontrolera punktów, jeœli wartoœæ punktów jest wiêksza od obecnych.
    /// </summary>
    /// <param name="points">Liczba punktów do przypisania.</param>
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
    /// Obs³uguje trafienie celu przez strza³ê i przypisuje odpowiedni¹ liczbê punktów w zale¿noœci od odleg³oœci od œrodka celu.
    /// </summary>
    /// <param name="arrowHittedTransform">Transform strza³y, która trafi³a w cel.</param>
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
