using UnityEngine;

public class PointController : MonoBehaviour
{
    private int achievedPoints = 0;

    private int pointsPerRound = 0;

    public void ClearPoints()
    {
        achievedPoints = 0;
        pointsPerRound = 0;
    }

    public void AddPointsPerRound(int points) {  pointsPerRound += points; }

    public void ClearPointsPerRound() { pointsPerRound = 0; }
    
    public int GetPointsPerRound() { return pointsPerRound; }


    public int GetAchievedPoints() {  
        return achievedPoints; 
    }

    public void AddPoints(int points) { achievedPoints += points; }
}
