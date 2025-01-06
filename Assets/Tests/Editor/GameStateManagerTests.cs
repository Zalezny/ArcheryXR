using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Klasa test�w jednostkowych dla klasy <see cref="GameStateManager"/>.
/// Testuje logik� zarz�dzania stanami gry, przypisywania punkt�w oraz inne kluczowe funkcjonalno�ci.
/// </summary>
public class GameStateManagerTests
{
    /// <summary>
    /// Instancja <see cref="GameStateManager"/> u�ywana w testach.
    /// </summary>
    private GameStateManager gameStateManager;

    /// <summary>
    /// Instancja <see cref="PointController"/> u�ywana do testowania przypisywania punkt�w.
    /// </summary>
    private PointController pointController;

    /// <summary>
    /// Lista poziom�w u�ywana w testach.
    /// </summary>
    private List<LevelModel> levels;

    /// <summary>
    /// Metoda uruchamiana przed ka�dym testem, inicjalizuje dane testowe.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        // Inicjalizacja danych testowych
        GameObject go = new GameObject("GameStateManager");
        gameStateManager = go.AddComponent<GameStateManager>();

        // Tworzymy przyk�adowy PointController
        GameObject pointControllerGO = new GameObject("PointController");
        pointController = pointControllerGO.AddComponent<PointController>();
        gameStateManager.pointController = pointController;

        // Tworzymy przyk�adowe poziomy
        levels = new List<LevelModel>
        {
            new("Level1", 10, 3, 2, 1f, 5),
            new("Level2", 10, 4, 2, 1f, 8)
        };
        gameStateManager.levels = levels;
    }


    /// <summary>
    /// Testuje metod� HasEnoughPoints. Powinno zwr�ci� <c>false</c>, gdy punkt�w jest za ma�o.
    /// </summary>
    [Test]
    public void HasEnoughPoints_ShouldReturnFalse_WhenPointsAreTooLow()
    {
        pointController.ClearPoints();
        pointController.AddPointsPerRound(3);

        bool result = InvokeHasEnoughPoints(levels[0]);
        Assert.IsFalse(result, "Amount of points is correct");
    }

    /// <summary>
    /// Testuje metod� HasEnoughPoints. Powinno zwr�ci� <c>true</c>, gdy punkt�w jest wystarczaj�co du�o.
    /// </summary>
    [Test]
    public void HasEnoughPoints_ShouldReturnTrue_WhenPointsAreSufficient()
    {
        pointController.ClearPointsPerRound();
        pointController.AddPointsPerRound(5);

        bool result = InvokeHasEnoughPoints(levels[0]);
        Assert.IsTrue(result, "Amount of points is correct");
    }

    /// <summary>
    /// Testuje metod� Remap. Powinna zwr�ci� <c>0</c>, gdy warto�� jest r�wna <c>fromMin</c>.
    /// </summary>
    [Test]
    public void Remap_ShouldReturnZero_WhenValueEqualsFromMin()
    {
        BowStringHandler bowHandler = new GameObject("BowHandler").AddComponent<BowStringHandler>();
        float result = InvokeRemap(bowHandler, 0f, 0f, 1f, 0f, 1f);
        Assert.AreEqual(0f, result, "Values are equal");
    }

    /// <summary>
    /// Testuje metod� Remap. Powinna zwr�ci� <c>1</c>, gdy warto�� jest r�wna <c>fromMax</c>.
    /// </summary>
    [Test]
    public void Remap_ShouldReturnOne_WhenValueEqualsFromMax()
    {
        GameObject bowHandler = new GameObject("Bow");
        var bowStringHandler = bowHandler.AddComponent<BowStringHandler>();

        float result = InvokeRemap(bowStringHandler, 1f, 0f, 1f, 0f, 1f);
        Assert.AreEqual(1f, result, "Values are equal");
    }

    /// <summary>
    /// Testuje metod� PrepareArrow w <see cref="ArrowController"/>.
    /// Sprawdza, czy wska�nik �rodka (MidPointIndicator) zostaje aktywowany.
    /// </summary>
    [Test]
    public void PrepareArrow_ShouldEnableMidVisualizer()
    {
        GameObject bow = new GameObject("Bow");
        ArrowController arrowController = bow.AddComponent<ArrowController>();
        GameObject midVisualizer = new GameObject("MidPoint");
        midVisualizer.SetActive(false);
        typeof(ArrowController).GetField("midStringVisualPoint",
          System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
          .SetValue(arrowController, midVisualizer);

        arrowController.PrepareArrow();
        Assert.IsTrue(midVisualizer.activeSelf, "MidPointIndicator is visible");
    }

    /// <summary>
    /// Pomocnicza metoda do testowania metody HasEnoughPoints za pomoc� odbicia lustrzanego (reflection).
    /// </summary>
    /// <param name="level">Model poziomu do testowania.</param>
    /// <returns>Wynik dzia�ania metody HasEnoughPoints.</returns>
    private bool InvokeHasEnoughPoints(LevelModel level)
    {
        var method = typeof(GameStateManager).GetMethod("HasEnoughPoints",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return (bool)method.Invoke(gameStateManager, new object[] { level });
    }

    /// <summary>
    /// Pomocnicza metoda do testowania metody Remap za pomoc� odbicia lustrzanego (reflection).
    /// </summary>
    /// <param name="handler">Instancja <see cref="BowStringHandler"/>.</param>
    /// <param name="value">Warto�� wej�ciowa.</param>
    /// <param name="fMin">Minimalna warto�� wej�ciowego zakresu.</param>
    /// <param name="fMax">Maksymalna warto�� wej�ciowego zakresu.</param>
    /// <param name="tMin">Minimalna warto�� wyj�ciowego zakresu.</param>
    /// <param name="tMax">Maksymalna warto�� wyj�ciowego zakresu.</param>
    /// <returns>Przeskalowana warto��.</returns>
    private float InvokeRemap(BowStringHandler handler, float value, float fMin, float fMax, float tMin, float tMax)
    {
        var method = typeof(BowStringHandler).GetMethod("Remap",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return (float)method.Invoke(handler, new object[] { value, fMin, fMax, tMin, tMax });
    }
}
