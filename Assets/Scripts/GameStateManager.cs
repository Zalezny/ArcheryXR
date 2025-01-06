using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Zarz�dza stanami gry, takimi jak ekran startowy, rozgrywka, ekran ko�cowy oraz logika poziom�w.
/// </summary>
public class GameStateManager : MonoBehaviour
{
    /// <summary>
    /// UI ekranu startowego gry.
    /// </summary>
    [SerializeField]
    private GameObject startUI;

    /// <summary>
    /// UI ekranu ko�cowego gry.
    /// </summary>
    [SerializeField]
    private GameObject endUI;

    /// <summary>
    /// G��wna scena gry.
    /// </summary>
    [SerializeField]
    private GameObject game;

    /// <summary>
    /// Lista modeli poziom�w definiuj�cych logik� gry.
    /// </summary>
    [SerializeField]
    public List<LevelModel> levels;

    /// <summary>
    /// Kontroler punkt�w, zarz�dza wynikami gracza.
    /// </summary>
    [SerializeField]
    public PointController pointController;

    /// <summary>
    /// Prefab spawnera cel�w, kt�ry jest generowany dla ka�dego poziomu.
    /// </summary>
    [SerializeField]
    private GameObject targetSpawnerPrefab;

    /// <summary>
    /// Aktualny indeks poziomu w rozgrywce.
    /// </summary>
    private int currentLevelIndex = 0;

    /// <summary>
    /// Obiekt spawnera cel�w dla bie��cego poziomu.
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
    /// Rozpoczyna gr� od pierwszego poziomu.
    /// </summary>
    public void StartGame()
    {
        startUI.SetActive(false);
        game.SetActive(true);
        endUI.SetActive(false);
        PlayLevel();
    }

    /// <summary>
    /// Wy�wietla ekran startowy i resetuje gr�.
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
    /// Wy�wietla ekran ko�cowy z wynikiem gracza.
    /// </summary>
    public void ShowEnd()
    {
        endUI.GetComponentInChildren<EndTextReader>().GetComponent<EndTextReader>().achievedScore = pointController.GetAchievedPoints();
        startUI.SetActive(false);
        game.SetActive(false);
        endUI.SetActive(true);
    }

    /// <summary>
    /// Ko�czy gr� i zamyka aplikacj�.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Obs�uguje zako�czenie sesji gry, w tym wyczyszczenie punkt�w i usuni�cie spawnera cel�w.
    /// </summary>
    void EndGameSession()
    {
        ShowEnd();
        Destroy(currentTargetSpawner);
        pointController.ClearPoints();

    }

    /// <summary>
    /// Generuje spawner cel�w na podstawie bie��cego poziomu.
    /// </summary>
    /// <param name="level">Model poziomu, kt�ry ma zosta� za�adowany.</param>
    void HandleGenerateLevel(LevelModel level)
    {
        currentTargetSpawner = Instantiate(targetSpawnerPrefab);

        TargetSpawner targetScript = currentTargetSpawner.GetComponent<TargetSpawner>();
        targetScript.spawnTimer = level.nextTargetDuration;
        targetScript.speed = level.targetSpeed;
        targetScript.targetsCount = level.targetCount;
    }

    /// <summary>
    /// Sprawdza, czy gracz zdoby� wystarczaj�c� liczb� punkt�w, aby uko�czy� poziom.
    /// </summary>
    /// <param name="level">Model poziomu do sprawdzenia.</param>
    /// <returns>True, je�li gracz zdoby� wystarczaj�c� liczb� punkt�w, inaczej False.</returns>
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
    /// Rozpoczyna rozgrywk� na bie��cym poziomie.
    /// </summary>
    void PlayLevel()
    {
        LevelModel level = levels[currentLevelIndex];
        HandleGenerateLevel(level);
    }

    /// <summary>
    /// Sprawdza wynik poziomu i zarz�dza przej�ciem do nast�pnego poziomu lub ko�cem gry.
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
/// Model poziomu, definiuj�cy parametry rozgrywki.
/// </summary>
[System.Serializable]
public class LevelModel
{
    /// <summary>
    /// Nazwa poziomu.
    /// </summary>
    public string levelName;

    /// <summary>
    /// Liczba cel�w na poziomie.
    /// </summary>
    public int targetCount;

    /// <summary>
    /// Czas mi�dzy pojawieniem si� kolejnych cel�w.
    /// </summary>
    public int nextTargetDuration;

    /// <summary>
    /// Pr�dko�� cel�w.
    /// </summary>
    public float targetSpeed;

    /// <summary>
    /// Minimalna liczba punkt�w potrzebna do uko�czenia poziomu.
    /// </summary>
    public int minimumPoints;

    /// <summary>
    /// Inicjalizuje model poziomu z podanymi parametrami.
    /// </summary>
    /// <param name="name">Nazwa poziomu.</param>
    /// <param name="targetCount">Liczba cel�w.</param>
    /// <param name="nextTargetDuration">Czas mi�dzy celami.</param>
    /// <param name="targetSpeed">Pr�dko�� cel�w.</param>
    /// <param name="minimumPoints">Minimalna liczba punkt�w do uko�czenia.</param>
    public LevelModel(string name, int time, int targetCount, int nextTargetDuration, float targetSpeed, int minimumPoints)
    {
        this.levelName = name;
        this.targetCount = targetCount;
        this.nextTargetDuration = nextTargetDuration;
        this.targetSpeed = targetSpeed;
        this.minimumPoints = minimumPoints;
    }
}
