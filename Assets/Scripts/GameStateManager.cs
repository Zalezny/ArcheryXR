using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Zarz¹dza stanami gry, takimi jak ekran startowy, rozgrywka, ekran koñcowy oraz logika poziomów.
/// </summary>
public class GameStateManager : MonoBehaviour
{
    /// <summary>
    /// UI ekranu startowego gry.
    /// </summary>
    [SerializeField]
    private GameObject startUI;

    /// <summary>
    /// UI ekranu koñcowego gry.
    /// </summary>
    [SerializeField]
    private GameObject endUI;

    /// <summary>
    /// G³ówna scena gry.
    /// </summary>
    [SerializeField]
    private GameObject game;

    /// <summary>
    /// Lista modeli poziomów definiuj¹cych logikê gry.
    /// </summary>
    [SerializeField]
    public List<LevelModel> levels;

    /// <summary>
    /// Kontroler punktów, zarz¹dza wynikami gracza.
    /// </summary>
    [SerializeField]
    public PointController pointController;

    /// <summary>
    /// Prefab spawnera celów, który jest generowany dla ka¿dego poziomu.
    /// </summary>
    [SerializeField]
    private GameObject targetSpawnerPrefab;

    /// <summary>
    /// Aktualny indeks poziomu w rozgrywce.
    /// </summary>
    private int currentLevelIndex = 0;

    /// <summary>
    /// Obiekt spawnera celów dla bie¿¹cego poziomu.
    /// </summary>
    private GameObject currentTargetSpawner;

    /// <summary>
    /// Inicjalizuje stany gry na ekran startowy.
    /// </summary>
    private void Start()
    {
        startUI.SetActive(true);
        endUI.SetActive(false);
        game.SetActive(false);
        currentLevelIndex = 0;
        pointController.ClearPoints();
        currentTargetSpawner = null;
    }

    /// <summary>
    /// Rozpoczyna grê od pierwszego poziomu.
    /// </summary>
    public void StartGame()
    {
        startUI.SetActive(false);
        game.SetActive(true);
        endUI.SetActive(false);
        PlayLevel();
    }

    /// <summary>
    /// Wyœwietla ekran startowy i resetuje grê.
    /// </summary>
    public void ShowMenu()
    {
        startUI.SetActive(true);
        game.SetActive(false);
        endUI.SetActive(false);
        pointController.ClearPoints();
        currentLevelIndex = 0;
    }

    /// <summary>
    /// Wyœwietla ekran koñcowy z wynikiem gracza.
    /// </summary>
    public void ShowEnd()
    {
        endUI.GetComponentInChildren<EndTextReader>().GetComponent<EndTextReader>().achievedScore = pointController.GetAchievedPoints();
        startUI.SetActive(false);
        game.SetActive(false);
        endUI.SetActive(true);
    }

    /// <summary>
    /// Koñczy grê i zamyka aplikacjê.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Obs³uguje zakoñczenie sesji gry, w tym wyczyszczenie punktów i usuniêcie spawnera celów.
    /// </summary>
    void EndGameSession()
    {
        ShowEnd();
        Destroy(currentTargetSpawner);
        pointController.ClearPoints();

    }

    /// <summary>
    /// Generuje spawner celów na podstawie bie¿¹cego poziomu.
    /// </summary>
    /// <param name="level">Model poziomu, który ma zostaæ za³adowany.</param>
    void HandleGenerateLevel(LevelModel level)
    {
        currentTargetSpawner = Instantiate(targetSpawnerPrefab);

        TargetSpawner targetScript = currentTargetSpawner.GetComponent<TargetSpawner>();
        targetScript.spawnTimer = level.nextTargetDuration;
        targetScript.speed = level.targetSpeed;
        targetScript.targetsCount = level.targetCount;
    }

    /// <summary>
    /// Sprawdza, czy gracz zdoby³ wystarczaj¹c¹ liczbê punktów, aby ukoñczyæ poziom.
    /// </summary>
    /// <param name="level">Model poziomu do sprawdzenia.</param>
    /// <returns>True, jeœli gracz zdoby³ wystarczaj¹c¹ liczbê punktów, inaczej False.</returns>
    bool HasEnoughPoints(LevelModel level)
    {
        int achievedPoints = pointController.GetPointsPerRound();

        if (achievedPoints >= level.minimumPoints)
        {
            return true;
        } else
        {
            return false;
        }
    }

    /// <summary>
    /// Rozpoczyna rozgrywkê na bie¿¹cym poziomie.
    /// </summary>
    void PlayLevel()
    {
        LevelModel level = levels[currentLevelIndex];
        HandleGenerateLevel(level);
    }

    /// <summary>
    /// Sprawdza wynik poziomu i zarz¹dza przejœciem do nastêpnego poziomu lub koñcem gry.
    /// </summary>
    public void CheckResultOfLevel()
    {

        var level = levels[currentLevelIndex];

        if (HasEnoughPoints(level))
        {
            pointController.AddPoints(pointController.GetPointsPerRound());
            pointController.ClearPointsPerRound();
            currentLevelIndex++;
            if (currentLevelIndex < levels.Count-1)
            {
                PlayLevel();
            }
            else
            {
                EndGameSession();
            }
        }
        else
        {
            pointController.ClearPointsPerRound();
            EndGameSession();
        }
    }


}

/// <summary>
/// Model poziomu, definiuj¹cy parametry rozgrywki.
/// </summary>
[System.Serializable]
public class LevelModel
{
    /// <summary>
    /// Nazwa poziomu.
    /// </summary>
    public string levelName;

    /// <summary>
    /// Liczba celów na poziomie.
    /// </summary>
    public int targetCount;

    /// <summary>
    /// Czas miêdzy pojawieniem siê kolejnych celów.
    /// </summary>
    public int nextTargetDuration;

    /// <summary>
    /// Prêdkoœæ celów.
    /// </summary>
    public float targetSpeed;

    /// <summary>
    /// Minimalna liczba punktów potrzebna do ukoñczenia poziomu.
    /// </summary>
    public int minimumPoints;

    /// <summary>
    /// Inicjalizuje model poziomu z podanymi parametrami.
    /// </summary>
    /// <param name="name">Nazwa poziomu.</param>
    /// <param name="targetCount">Liczba celów.</param>
    /// <param name="nextTargetDuration">Czas miêdzy celami.</param>
    /// <param name="targetSpeed">Prêdkoœæ celów.</param>
    /// <param name="minimumPoints">Minimalna liczba punktów do ukoñczenia.</param>
    public LevelModel(string name, int time, int targetCount, int nextTargetDuration, float targetSpeed, int minimumPoints)
    {
        this.levelName = name;
        this.targetCount = targetCount;
        this.nextTargetDuration = nextTargetDuration;
        this.targetSpeed = targetSpeed;
        this.minimumPoints = minimumPoints;
    }
}
