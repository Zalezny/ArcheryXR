using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;

/// <summary>
/// Zarz¹dza generowaniem celów w wirtualnym œrodowisku MR (Mixed Reality),
/// z uwzglêdnieniem losowych pozycji na œcianach w przestrzeni.
/// </summary>
public class TargetSpawner : MonoBehaviour
{
    /// <summary>
    /// Interwa³ czasowy miêdzy generowaniem kolejnych celów (w sekundach).
    /// </summary>
    public float spawnTimer = 10;

    /// <summary>
    /// Prefab obiektu celu, który ma zostaæ wygenerowany.
    /// </summary>
    public GameObject prefabTarget;

    /// <summary>
    /// Minimalna odleg³oœæ celu od krawêdzi powierzchni.
    /// </summary>
    public float minEdgeDistance = 0.3f;

    /// <summary>
    /// Etykiety sceny, które okreœlaj¹, gdzie cele mog¹ byæ generowane.
    /// </summary>
    public MRUKAnchor.SceneLabels spawnLabels;

    /// <summary>
    /// Przesuniêcie celu wzd³u¿ normalnej powierzchni.
    /// </summary>
    public float normalOffset;

    /// <summary>
    /// Minimalna wysokoœæ generowanego celu (oœ Y).
    /// </summary>
    public float minimumPositionY = 0.5f;

    /// <summary>
    /// Maksymalna liczba prób generowania celu w losowym miejscu.
    /// </summary>
    public int spawnTry = 1000;

    /// <summary>
    /// Prêdkoœæ poruszania siê celu.
    /// </summary>
    public float speed = 1f;

    /// <summary>
    /// Prêdkoœæ poruszania siê celu.
    /// </summary>
    public int targetsCount = 1;

    /// <summary>
    /// Licznik wygenerowanych celów.
    /// </summary>
    private int spawnedTargets = 0;

    /// <summary>
    /// Licznik czasu u¿ywany do kontrolowania interwa³ów generowania celów.
    /// </summary>
    private float timer;

    /// <summary>
    /// Pozycja najni¿szej œciany w pomieszczeniu.
    /// </summary>
    private Vector3? smallestWallPosition;

    /// <summary>
    /// Wywo³ywana w ka¿dej klatce, zarz¹dza logik¹ generowania celów w okreœlonych interwa³ach.
    /// </summary>
    void Update()
    {
        if (!MRUK.Instance && !MRUK.Instance.IsInitialized)
        {
            return;
        }

        MRUKRoom room = MRUK.Instance.GetCurrentRoom();

        if (smallestWallPosition == null) {
            smallestWallPosition = CalcSmallestWallTransform(room); 
        }

        timer += Time.deltaTime;
        if (timer > spawnTimer)
        {
            SpawnTarget(room);
            timer -= spawnTimer;
        }
    }

    /// <summary>
    /// Generuje nowy cel w przestrzeni, jeœli docelowa liczba celów nie zosta³a osi¹gniêta.
    /// </summary>
    /// <param name="room">Obiekt pomieszczenia z informacjami o œcianach i powierzchniach.</param>
    public void SpawnTarget(MRUKRoom room)
    {
        if (spawnedTargets >= targetsCount)
        {
            return;
        }

        int currentTry = 0;

        while (currentTry < spawnTry) {
            

            bool hasPosition = room.GenerateRandomPositionOnSurface(MRUK.SurfaceType.VERTICAL, minEdgeDistance, LabelFilter.Included(spawnLabels), out Vector3 pos, out Vector3 norm);

            if (hasPosition)
            {
                bool isLastTarget = false;
                if(spawnedTargets >= (targetsCount - 1))
                {
                    isLastTarget = true;
                }
                Vector3 randomPositionNormalOffset = pos + norm * normalOffset;
                if (randomPositionNormalOffset.y < minimumPositionY)
                {
                    randomPositionNormalOffset.y = UnityEngine.Random.Range(minimumPositionY, smallestWallPosition?.y ?? 1);
                }
                GameObject targetObject = Instantiate(prefabTarget, randomPositionNormalOffset, Quaternion.identity);
                var targetController = targetObject.GetComponent<TargetController>();
                targetController.speed = speed;
                targetController.isLastTarget = isLastTarget;
                targetController.StartMoving(norm);
                spawnedTargets++;
                return;
            }
            else
            {
                currentTry++;
            }
        }

        
    }

    /// <summary>
    /// Oblicza pozycjê najni¿szej œciany w pomieszczeniu.
    /// </summary>
    /// <param name="room">Obiekt pomieszczenia z list¹ œcian.</param>
    /// <returns>Pozycja najni¿szej œciany w przestrzeni.</returns>
    private Vector3 CalcSmallestWallTransform(MRUKRoom room)
    {
        List<MRUKAnchor> walls = room.WallAnchors;
        Vector3 smallestHeightPosition = new Vector3(99,99,99);
        foreach (MRUKAnchor wall in walls)
        {
            Vector3 position = wall.transform.position;
            if (smallestHeightPosition.y > position.y)
            {
                smallestHeightPosition = position;
            }
        }
        return smallestHeightPosition;
    }
}
