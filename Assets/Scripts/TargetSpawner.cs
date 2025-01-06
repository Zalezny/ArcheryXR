using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;

/// <summary>
/// Zarz�dza generowaniem cel�w w wirtualnym �rodowisku MR (Mixed Reality),
/// z uwzgl�dnieniem losowych pozycji na �cianach w przestrzeni.
/// </summary>
public class TargetSpawner : MonoBehaviour
{
    /// <summary>
    /// Interwa� czasowy mi�dzy generowaniem kolejnych cel�w (w sekundach).
    /// </summary>
    public float spawnTimer = 10;

    /// <summary>
    /// Prefab obiektu celu, kt�ry ma zosta� wygenerowany.
    /// </summary>
    public GameObject prefabTarget;

    /// <summary>
    /// Minimalna odleg�o�� celu od kraw�dzi powierzchni.
    /// </summary>
    public float minEdgeDistance = 0.3f;

    /// <summary>
    /// Etykiety sceny, kt�re okre�laj�, gdzie cele mog� by� generowane.
    /// </summary>
    public MRUKAnchor.SceneLabels spawnLabels;

    /// <summary>
    /// Przesuni�cie celu wzd�u� normalnej powierzchni.
    /// </summary>
    public float normalOffset;

    /// <summary>
    /// Minimalna wysoko�� generowanego celu (o� Y).
    /// </summary>
    public float minimumPositionY = 0.5f;

    /// <summary>
    /// Maksymalna liczba pr�b generowania celu w losowym miejscu.
    /// </summary>
    public int spawnTry = 1000;

    /// <summary>
    /// Pr�dko�� poruszania si� celu.
    /// </summary>
    public float speed = 1f;

    /// <summary>
    /// Pr�dko�� poruszania si� celu.
    /// </summary>
    public int targetsCount = 1;

    /// <summary>
    /// Licznik wygenerowanych cel�w.
    /// </summary>
    private int spawnedTargets = 0;

    /// <summary>
    /// Licznik czasu u�ywany do kontrolowania interwa��w generowania cel�w.
    /// </summary>
    private float timer;

    /// <summary>
    /// Pozycja najni�szej �ciany w pomieszczeniu.
    /// </summary>
    private Vector3? smallestWallPosition;

    /// <summary>
    /// Wywo�ywana w ka�dej klatce, zarz�dza logik� generowania cel�w w okre�lonych interwa�ach.
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
    /// Generuje nowy cel w przestrzeni, je�li docelowa liczba cel�w nie zosta�a osi�gni�ta.
    /// </summary>
    /// <param name="room">Obiekt pomieszczenia z informacjami o �cianach i powierzchniach.</param>
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
    /// Oblicza pozycj� najni�szej �ciany w pomieszczeniu.
    /// </summary>
    /// <param name="room">Obiekt pomieszczenia z list� �cian.</param>
    /// <returns>Pozycja najni�szej �ciany w przestrzeni.</returns>
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
