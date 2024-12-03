using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using NUnit.Framework;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public float spawnTimer = 10;
    public GameObject prefabTarget;

    public float minEdgeDistance = 0.3f;
    public MRUKAnchor.SceneLabels spawnLabels;
    public float normalOffset;

    public float minimumPositionY = 0.5f;

    public int spawnTry = 1000;

    private float timer;

    private Vector3? smallestWallPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
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

    public void SpawnTarget(MRUKRoom room)
    {
        

        int currentTry = 0;

        while (currentTry < spawnTry) {
            bool hasPosition = room.GenerateRandomPositionOnSurface(MRUK.SurfaceType.VERTICAL, minEdgeDistance, LabelFilter.Included(spawnLabels), out Vector3 pos, out Vector3 norm);
            if (hasPosition)
            {
                Vector3 randomPositionNormalOffset = pos + norm * normalOffset;
                if (randomPositionNormalOffset.y < minimumPositionY)
                {
                    randomPositionNormalOffset.y = UnityEngine.Random.Range(minimumPositionY, smallestWallPosition?.y ?? 1);
                }
                GameObject targetObject = Instantiate(prefabTarget, randomPositionNormalOffset, Quaternion.identity);
                targetObject.GetComponent<TargetController>().StartMoving(norm);
                return;
            }
            else
            {
                currentTry++;
            }
        }

        
    }


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
