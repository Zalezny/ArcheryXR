using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
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
    public GameObject pointControllerPrefab;
    [SerializeField]
    public GameObject targetSpawnerPrefab;

    private int currentLevelIndex = 0;

    private GameObject currentPointerController;
    private GameObject currentTargetSpawner;


    private void Start()
    {
        startUI.SetActive(true);
        endUI.SetActive(false);
        game.SetActive(false);
        currentLevelIndex = 0;
        currentPointerController = null;
        currentTargetSpawner = null;

        if (levels.Count <= 0)
        {
            Assert.Fail("Zdefiniuj levele gry");
        }
    }

    public void StartGame()
    {
        startUI.SetActive(false);
        game.SetActive(true);
        endUI.SetActive(false);
        StartCoroutine(PlayLevel());
    }

    public void ShowMenu()
    {
        startUI.SetActive(true);
        game.SetActive(false);
        endUI.SetActive(false);
        currentPointerController = null;
        currentLevelIndex = 0;
    }

    public void ShowEnd()
    {
        endUI.GetComponent<EndTextReader>().achievedScore = currentPointerController.GetComponent<PointController>().GetAchievedPoints();
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
        
        StopCoroutine(PlayLevel());
        ShowEnd();
        Destroy(currentPointerController);
        Destroy(currentTargetSpawner);
        currentPointerController = null;
        currentPointerController = null;
    }

    void HandleGenerateLevel(LevelModel level)
    {
        currentPointerController = Instantiate(pointControllerPrefab);
        currentTargetSpawner = Instantiate(targetSpawnerPrefab);

        TargetSpawner targetScript = currentTargetSpawner.GetComponent<TargetSpawner>();
        targetScript.spawnTimer = level.nextTargetDuration;
        targetScript.speed = level.targetSpeed;
    }

    bool HasEnoughPoints(LevelModel level)
    {
        int achievedPoints = currentPointerController.GetComponent<PointController>().GetAchievedPoints();

        if (achievedPoints >= level.minimumPoints)
        {
            return true;
        } else
        {
            return false;
        }
    }

    IEnumerator PlayLevel()
    {
        LevelModel level = levels[currentLevelIndex];
        HandleGenerateLevel(level);

        // Czekaj 10 sekund
        yield return new WaitForSeconds(10f);

        //Obsluz koniec danego level'a
        if (HasEnoughPoints(level)) {
            // PrzejdŸ do nastêpnego poziomu
            currentLevelIndex++;
            if (currentLevelIndex < levels.Count)
            {
                StartCoroutine(PlayLevel()); // Rozpocznij nowy poziom
            }
            else
            {
                EndGameSession();
                Debug.Log("Wszystkie poziomy zosta³y ukoñczone!");
                // Mo¿esz tutaj dodaæ logikê koñca gry lub restartu
            }
        } else
        {
            EndGameSession();

        }

       
    }


}
[System.Serializable] // Umo¿liwia serializacjê w inspektorze
public class LevelModel
{
    public string levelName; // Nazwa poziomu
    public int time;
    public int targetCount;
    public int nextTargetDuration;
    public int targetSpeed;
    public int minimumPoints;
    public LevelModel(string name, int time, int targetCount, int nextTargetDuration, int targetSpeed, int minimumPoints)
    {
        this.time = time;
        this.levelName = name;
        this.time = time;
        this.targetCount = targetCount;
        this.nextTargetDuration = nextTargetDuration;
        this.targetSpeed = targetSpeed;
        this.minimumPoints = minimumPoints;

    }
}
