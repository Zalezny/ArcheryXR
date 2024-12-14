using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    public GameObject startUI;
    [SerializeField]
    public GameObject endUI;
    [SerializeField]
    public GameObject game;


    [SerializeField]
    public List<LevelModel> levels;
    [SerializeField]
    public PointController pointController;
    [SerializeField]
    public GameObject targetSpawnerPrefab;

    private int currentLevelIndex = 0;

    private GameObject currentTargetSpawner;


    private void Start()
    {
        startUI.SetActive(true);
        endUI.SetActive(false);
        game.SetActive(false);
        currentLevelIndex = 0;
        pointController.ClearPoints();
        currentTargetSpawner = null;
    }

    public void StartGame()
    {
        startUI.SetActive(false);
        game.SetActive(true);
        endUI.SetActive(false);
        PlayLevel();
    }

    public void ShowMenu()
    {
        startUI.SetActive(true);
        game.SetActive(false);
        endUI.SetActive(false);
        pointController.ClearPoints();
        currentLevelIndex = 0;
    }

    public void ShowEnd()
    {
        endUI.GetComponentInChildren<EndTextReader>().GetComponent<EndTextReader>().achievedScore = pointController.GetAchievedPoints();
        startUI.SetActive(false);
        game.SetActive(false);
        endUI.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void EndGameSession()
    {
        ShowEnd();
        Destroy(currentTargetSpawner);
        pointController.ClearPoints();

    }

    void HandleGenerateLevel(LevelModel level)
    {
        currentTargetSpawner = Instantiate(targetSpawnerPrefab);

        TargetSpawner targetScript = currentTargetSpawner.GetComponent<TargetSpawner>();
        targetScript.spawnTimer = level.nextTargetDuration;
        targetScript.speed = level.targetSpeed;
        targetScript.targetsCount = level.targetCount;
    }

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

    void PlayLevel()
    {
        LevelModel level = levels[currentLevelIndex];
        HandleGenerateLevel(level);

    }

    public void CheckResultOfLevel()
    {
        UnityEngine.Debug.Log("Test CheckResultOfLevel");

        var level = levels[currentLevelIndex];

        //Obsluz koniec danego level'a
        if (HasEnoughPoints(level))
        {
            pointController.AddPoints(pointController.GetPointsPerRound());
            pointController.ClearPointsPerRound();
            // Przejd� do nast�pnego poziomu
            currentLevelIndex++;
            if (currentLevelIndex < levels.Count-1)
            {
                PlayLevel(); // Rozpocznij nowy poziom
            }
            else
            {
                EndGameSession();
                Debug.Log("Test Wszystkie poziomy zosta�y uko�czone!");
                // Mo�esz tutaj doda� logik� ko�ca gry lub restartu
            }
        }
        else
        {
            pointController.ClearPointsPerRound();
            EndGameSession();
            Debug.Log("Test Uko�czenie z powodu braku wystarczaj�cej liczby punkt�w");


        }
    }


}
[System.Serializable] // Umo�liwia serializacj� w inspektorze
public class LevelModel
{
    public string levelName; // Nazwa poziomu
    public int targetCount;
    public int nextTargetDuration;
    public float targetSpeed;
    public int minimumPoints;
    public LevelModel(string name, int time, int targetCount, int nextTargetDuration, float targetSpeed, int minimumPoints)
    {
        this.levelName = name;
        this.targetCount = targetCount;
        this.nextTargetDuration = nextTargetDuration;
        this.targetSpeed = targetSpeed;
        this.minimumPoints = minimumPoints;

    }
}
