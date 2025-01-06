using UnityEngine;

/// <summary>
/// Zarz¹dza punktami zdobytymi przez gracza oraz punktami zdobytymi w bie¿¹cej rundzie.
/// </summary>
public class PointController : MonoBehaviour
{
    /// <summary>
    /// Ca³kowita liczba punktów zdobytych przez gracza.
    /// </summary>
    private int achievedPoints = 0;

    /// <summary>
    /// Liczba punktów zdobytych przez gracza w bie¿¹cej rundzie.
    /// </summary>
    private int pointsPerRound = 0;

    /// <summary>
    /// Czyœci wszystkie punkty (zarówno ogólne, jak i dla bie¿¹cej rundy).
    /// </summary>
    public void ClearPoints()
    {
        achievedPoints = 0;
        pointsPerRound = 0;
    }

    /// <summary>
    /// Dodaje punkty do bie¿¹cej rundy.
    /// </summary>
    /// <param name="points">Liczba punktów do dodania.</param>
    public void AddPointsPerRound(int points) {  pointsPerRound += points; }

    /// <summary>
    /// Czyœci punkty zdobyte w bie¿¹cej rundzie.
    /// </summary>
    public void ClearPointsPerRound() { pointsPerRound = 0; }

    /// <summary>
    /// Pobiera liczbê punktów zdobytych w bie¿¹cej rundzie.
    /// </summary>
    /// <returns>Liczba punktów zdobytych w bie¿¹cej rundzie.</returns>
    public int GetPointsPerRound() { return pointsPerRound; }

    /// <summary>
    /// Pobiera ca³kowit¹ liczbê punktów zdobytych przez gracza.
    /// </summary>
    /// <returns>Ca³kowita liczba zdobytych punktów.</returns>
    public int GetAchievedPoints() {  
        return achievedPoints; 
    }

    /// <summary>
    /// Dodaje punkty do ca³kowitego wyniku gracza.
    /// </summary>
    /// <param name="points">Liczba punktów do dodania.</param>
    public void AddPoints(int points) { achievedPoints += points; }
}
