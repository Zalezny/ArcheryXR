using UnityEngine;

public class PointController : MonoBehaviour
{
    private int achievedPoints;


    public int GetAchievedPoints() {  return achievedPoints; }

    public void addPoints(int points) { achievedPoints += points; }
}
