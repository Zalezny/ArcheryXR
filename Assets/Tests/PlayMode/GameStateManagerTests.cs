//using NUnit.Framework;
//using UnityEngine;
//using System.Collections.Generic;

//public class GameStateManagerTests
//{
//    private GameStateManager gameStateManager;
//    private PointController pointController;
//    private List<LevelModel> levels;

//    [SetUp]
//    public void SetUp()
//    {
//        // Inicjalizacja danych testowych
//        GameObject go = new GameObject("GameStateManager");
//        gameStateManager = go.AddComponent<GameStateManager>();

//        // Tworzymy przyk³adowy PointController
//        GameObject pointControllerGO = new GameObject("PointController");
//        pointController = pointControllerGO.AddComponent<PointController>();
//        gameStateManager.pointController = pointController;

//        // Tworzymy przyk³adowe poziomy
//        levels = new List<LevelModel>
//        {
//            new LevelModel("Level1", 10, 3, 2, 1f, 5),
//            new LevelModel("Level2", 10, 4, 2, 1f, 8)
//        };
//        gameStateManager.levels = levels;
//    }



//    [Test]
//    public void HasEnoughPoints_ShouldReturnFalse_WhenPointsAreTooLow()
//    {
//        pointController.ClearPoints();
//        pointController.AddPointsPerRound(3);

//        bool result = InvokeHasEnoughPoints(levels[0]);
//        Assert.IsFalse(result, "Amount of points is correct");
//    }

//    [Test]
//    public void HasEnoughPoints_ShouldReturnTrue_WhenPointsAreSufficient()
//    {
//        pointController.ClearPointsPerRound();
//        pointController.AddPointsPerRound(5);

//        bool result = InvokeHasEnoughPoints(levels[0]);
//        Assert.IsTrue(result, "Amount of points is correct");
//    }

//    [Test]
//    public void Remap_ShouldReturnZero_WhenValueEqualsFromMin()
//    {
//        BowStringHandler bowHandler = new GameObject("BowHandler").AddComponent<BowStringHandler>();
//        float result = InvokeRemap(bowHandler, 0f, 0f, 1f, 0f, 1f);
//        Assert.AreEqual(0f, result, "Values are equal");
//    }

//    [Test]
//    public void Remap_ShouldReturnOne_WhenValueEqualsFromMax()
//    {
//        GameObject bowHandler = new GameObject("Bow");
//        var bowStringHandler = bowHandler.AddComponent<BowStringHandler>();
       
//        float result = InvokeRemap(bowStringHandler, 1f, 0f, 1f, 0f, 1f);
//        Assert.AreEqual(1f, result, "Values are equal");
//    }

//    [Test]
//    public void PrepareArrow_ShouldEnableMidVisualizer()
//    {
//        GameObject bow = new GameObject("Bow");
//        ArrowController arrowController = bow.AddComponent<ArrowController>();
//        GameObject midVisualizer = new GameObject("MidPoint");
//        midVisualizer.SetActive(false);
//        typeof(ArrowController).GetField("midPointIndicator",
//          System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
//          .SetValue(arrowController, midVisualizer);

//        arrowController.PrepareArrow();
//        Assert.IsTrue(midVisualizer.activeSelf, "MidPointIndicator is visible");
//    }

//    // Pomocnicza metoda do testów HasEnoughPoints
//    private bool InvokeHasEnoughPoints(LevelModel level)
//    {
//        // Odbicie lustrzane, poniewa¿ HasEnoughPoints jest metod¹ prywatn¹ lub wewnêtrzn¹, 
//        // jeœli by³aby publiczna, mo¿na by wywo³aæ j¹ bezpoœrednio.
//        var method = typeof(GameStateManager).GetMethod("HasEnoughPoints",
//            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
//        return (bool)method.Invoke(gameStateManager, new object[] { level });
//    }

//    // Pomocnicza metoda do testów Remap (równie¿ wymaga reflection, jeœli jest private)
//    private float InvokeRemap(BowStringHandler handler, float value, float fMin, float fMax, float tMin, float tMax)
//    {
//        var method = typeof(BowStringHandler).GetMethod("Remap",
//            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
//        return (float)method.Invoke(handler, new object[] { value, fMin, fMax, tMin, tMax });
//    }
//}
