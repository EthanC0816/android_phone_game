using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessChunkSpawner : MonoBehaviour
{
    [Header("Chunk Settings")]
    public List<GameObject> chunkPrefabs;
    public Transform spawnPoint;
    public float chunkMoveSpeed = 5f;

    private Queue<GameObject> activeChunks = new Queue<GameObject>();
    public int maxActiveChunks = 5;
    private Transform lastChunkEnd;
   
    public bool gameStarted = false;

    void Start()
    {
        StartGame();
        gameStarted = true;
    }

    void Update()
    {

        if (gameStarted)
        {
            MoveChunks();
            ManageChunks();
        }
    }

    void StartGame()
    {

        for (int i = 0; i < maxActiveChunks; i++)
        {
            SpawnChunk();
        }

    }

    void SpawnChunk()
    {
        GameObject chunkPrefab = (activeChunks.Count == 0) ? chunkPrefabs[0] : chunkPrefabs[Random.Range(0, chunkPrefabs.Count)];
        GameObject newChunk = Instantiate(chunkPrefab);

        if (activeChunks.Count == 0)
        {
            newChunk.transform.position = spawnPoint.position;
        }
        else if (lastChunkEnd != null)
        {
            newChunk.transform.position = lastChunkEnd.position;
        }

        lastChunkEnd = newChunk.transform.Find("EndPoint");

        if (lastChunkEnd == null)
        {
            Debug.LogError("Chunk prefab missing an 'EndPoint' child object.");
        }

        activeChunks.Enqueue(newChunk);
    }

    void MoveChunks()
    {
        foreach (GameObject chunk in activeChunks)
        {
            chunk.transform.position += Vector3.left * chunkMoveSpeed * Time.deltaTime;
        }
    }

    void ManageChunks()
    {
        if (activeChunks.Count > 0)
        {
            GameObject firstChunk = activeChunks.Peek();
            if (firstChunk.transform.position.x < -50f)
            {
                Destroy(activeChunks.Dequeue());
                SpawnChunk();
            }
        }
    }

}
