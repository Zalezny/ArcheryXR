using UnityEngine;

/// <summary>
/// Zarz�dza punktami zdobytymi przez gracza oraz punktami zdobytymi w bie��cej rundzie.
/// </summary>
public class PointController : MonoBehaviour
{
    /// <summary>
    /// Ca�kowita liczba punkt�w zdobytych przez gracza.
    /// </summary>
    private int achievedPoints = 0;

    /// <summary>
    /// Liczba punkt�w zdobytych przez gracza w bie��cej rundzie.
    /// </summary>
    private int pointsPerRound = 0;

    /// <summary>
    /// Czy�ci wszystkie punkty (zar�wno og�lne, jak i dla bie��cej rundy).
    /// </summary>
    public void ClearPoints()
    {
        achievedPoints = 0;
        pointsPerRound = 0;
    }

    /// <summary>
    /// Dodaje punkty do bie��cej rundy.
    /// </summary>
    /// <param name="points">Liczba punkt�w do dodania.</param>
    public void AddPointsPerRound(int points) {  pointsPerRound += points; }

    /// <summary>
    /// Czy�ci punkty zdobyte w bie��cej rundzie.
    /// </summary>
    public void ClearPointsPerRound() { pointsPerRound = 0; }

    /// <summary>
    /// Pobiera liczb� punkt�w zdobytych w bie��cej rundzie.
    /// </summary>
    /// <returns>Liczba punkt�w zdobytych w bie��cej rundzie.</returns>
    public int GetPointsPerRound() { return pointsPerRound; }

    /// <summary>
    /// Pobiera ca�kowit� liczb� punkt�w zdobytych przez gracza.
    /// </summary>
    /// <returns>Ca�kowita liczba zdobytych punkt�w.</returns>
    public int GetAchievedPoints() {  
        return achievedPoints; 
    }

    /// <summary>
    /// Dodaje punkty do ca�kowitego wyniku gracza.
    /// </summary>
    /// <param name="points">Liczba punkt�w do dodania.</param>
    public void AddPoints(int points) { achievedPoints += points; }
}
