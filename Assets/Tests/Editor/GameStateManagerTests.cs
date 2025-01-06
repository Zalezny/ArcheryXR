using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Klasa testów jednostkowych dla klasy <see cref="GameStateManager"/>.
/// Testuje logikê zarz¹dzania stanami gry, przypisywania punktów oraz inne kluczowe funkcjonalnoœci.
/// </summary>
public class GameStateManagerTests
{
    /// <summary>
    /// Instancja <see cref="GameStateManager"/> u¿ywana w testach.
    /// </summary>
    private GameStateManager gameStateManager;

    /// <summary>
    /// Instancja <see cref="PointController"/> u¿ywana do testowania przypisywania punktów.
    /// </summary>
    private PointController pointController;

    /// <summary>
    /// Lista poziomów u¿ywana w testach.
    /// </summary>
    private List<LevelModel> levels;

    /// <summary>
    /// Metoda uruchamiana przed ka¿dym testem, inicjalizuje dane testowe.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        // Inicjalizacja danych testowych
        GameObject go = new GameObject("GameStateManager");
        gameStateManager = go.AddComponent<GameStateManager>();

        // Tworzymy przyk³adowy PointController
        GameObject pointControllerGO = new GameObject("PointController");
        pointController = pointControllerGO.AddComponent<PointController>();
        gameStateManager.pointController = pointController;

        // Tworzymy przyk³adowe poziomy
        levels = new List<LevelModel>
        {
            new("Level1", 10, 3, 2, 1f, 5),
            new("Level2", 10, 4, 2, 1f, 8)
        };
        gameStateManager.levels = levels;
    }


    /// <summary>
    /// Testuje metodê HasEnoughPoints. Powinno zwróciæ <c>false</c>, gdy punktów jest za ma³o.
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
    /// Testuje metodê HasEnoughPoints. Powinno zwróciæ <c>true</c>, gdy punktów jest wystarczaj¹co du¿o.
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
    /// Testuje metodê Remap. Powinna zwróciæ <c>0</c>, gdy wartoœæ jest równa <c>fromMin</c>.
    /// </summary>
    [Test]
    public void Remap_ShouldReturnZero_WhenValueEqualsFromMin()
    {
        BowStringHandler bowHandler = new GameObject("BowHandler").AddComponent<BowStringHandler>();
        float result = InvokeRemap(bowHandler, 0f, 0f, 1f, 0f, 1f);
        Assert.AreEqual(0f, result, "Values are equal");
    }

    /// <summary>
    /// Testuje metodê Remap. Powinna zwróciæ <c>1</c>, gdy wartoœæ jest równa <c>fromMax</c>.
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
    /// Testuje metodê PrepareArrow w <see cref="ArrowController"/>.
    /// Sprawdza, czy wskaŸnik œrodka (MidPointIndicator) zostaje aktywowany.
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
    /// Pomocnicza metoda do testowania metody HasEnoughPoints za pomoc¹ odbicia lustrzanego (reflection).
    /// </summary>
    /// <param name="level">Model poziomu do testowania.</param>
    /// <returns>Wynik dzia³ania metody HasEnoughPoints.</returns>
    private bool InvokeHasEnoughPoints(LevelModel level)
    {
        var method = typeof(GameStateManager).GetMethod("HasEnoughPoints",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return (bool)method.Invoke(gameStateManager, new object[] { level });
    }

    /// <summary>
    /// Pomocnicza metoda do testowania metody Remap za pomoc¹ odbicia lustrzanego (reflection).
    /// </summary>
    /// <param name="handler">Instancja <see cref="BowStringHandler"/>.</param>
    /// <param name="value">Wartoœæ wejœciowa.</param>
    /// <param name="fMin">Minimalna wartoœæ wejœciowego zakresu.</param>
    /// <param name="fMax">Maksymalna wartoœæ wejœciowego zakresu.</param>
    /// <param name="tMin">Minimalna wartoœæ wyjœciowego zakresu.</param>
    /// <param name="tMax">Maksymalna wartoœæ wyjœciowego zakresu.</param>
    /// <returns>Przeskalowana wartoœæ.</returns>
    private float InvokeRemap(BowStringHandler handler, float value, float fMin, float fMax, float tMin, float tMax)
    {
        var method = typeof(BowStringHandler).GetMethod("Remap",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return (float)method.Invoke(handler, new object[] { value, fMin, fMax, tMin, tMax });
    }
}
