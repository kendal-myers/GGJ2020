using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AsteroidField : MonoBehaviour
{
    public Transform container;
    public GameObject[] asteroids;
    public int chunkSize;
    public int chunksLoaded = 3;
    public float density;
    public Vector3 jitter = Vector3.one;
    public Vector3 drift = Vector3.one;
    

    public Dictionary<Vector3Int, GameObject> chunks = new Dictionary<Vector3Int, GameObject>();
    private List<GameObject> disabledChunks = new List<GameObject>();
    
    private void FixedUpdate()
    {        
        GenerateAsteroidField();
        ClearChunks();
        int asteroidCount = 0;
        foreach(var key in chunks.Keys)
        {
            asteroidCount += chunks[key].transform.childCount;
        }
        Debug.Log($"Asteroid count: {asteroidCount}");
    }

    private Vector3 lastPosition;
    private void ClearChunks()
    {
        var change = lastPosition - transform.position;
        if (!Mathf.Approximately(change.x, 0f))
        {            
            int x = (int)Mathf.Sign(change.x) * (chunksLoaded + 1);
            for(int y = -chunksLoaded; y <= chunksLoaded; y++)
            {
                for(int z = -chunksLoaded; z <= chunksLoaded; z++)
                {
                    var chunk = GetChunk(x, y, z);
                    if (chunks.ContainsKey(chunk))
                    {
                        Destroy(chunks[chunk]);
                        chunks.Remove(chunk);
                    }
                }
            }
        }

        if (!Mathf.Approximately(change.y, 0f))
        {
            int y = (int)Mathf.Sign(change.y) * (chunksLoaded + 1);
            for (int x = -chunksLoaded; x <= chunksLoaded; x++)
            {
                for (int z = -chunksLoaded; z <= chunksLoaded; z++)
                {
                    var chunk = GetChunk(x, y, z);
                    if (chunks.ContainsKey(chunk))
                    {
                        Destroy(chunks[chunk]);
                        chunks.Remove(chunk);
                    }
                }
            }
        }

        if (!Mathf.Approximately(change.z, 0f))
        {
            int z = (int)Mathf.Sign(change.z) * (chunksLoaded + 1);
            for (int x = -chunksLoaded; x <= chunksLoaded; x++)
            {
                for (int y = -chunksLoaded; y <= chunksLoaded; y++)
                {
                    var chunk = GetChunk(x, y, z);
                    if (chunks.ContainsKey(chunk))
                    {
                        Destroy(chunks[chunk]);
                        chunks.Remove(chunk);
                    }
                }
            }
        }
        lastPosition = transform.position;
    }

    public void GenerateAsteroidField()
    {
        var previousChunks = chunks.Keys.ToList();
        for (int x = -chunksLoaded; x <= chunksLoaded; x++)
        {
            for (int y = -chunksLoaded; y <= chunksLoaded; y++)
            {
                for (int z = -chunksLoaded; z <= chunksLoaded; z++)
                {
                    var chunk = GetChunk(x, y, z);
                    GenerateChunk(chunk);
                    previousChunks.Remove(chunk);
                }
            }
        }
        foreach(var chunk in previousChunks)
        {
            chunks[chunk].SetActive(false);
            //chunks.Remove(chunk);
        }
    }

    private Vector3Int GetChunk(int x, int y, int z)
    {
        return new Vector3Int(Mathf.FloorToInt((transform.position.x + (x * chunkSize)) / chunkSize),
            Mathf.FloorToInt((transform.position.y + (y * chunkSize)) / chunkSize),
            Mathf.FloorToInt((transform.position.z + (z * chunkSize)) / chunkSize));
    }

    public void GenerateChunk(Vector3Int chunk)
    {
        //Do not regenerate existing chunks
        if (chunks.ContainsKey(chunk))
        {
            chunks[chunk].SetActive(true);
            return;
        }

        var chunkContainer = new GameObject("Chunk " + chunk.ToString());
        chunkContainer.transform.position = chunk * chunkSize;
        chunks.Add(chunk, chunkContainer);

        for (float offsetX = chunk.x * chunkSize; offsetX < (chunk.x + 1) * chunkSize; offsetX += density)
        {
            for (float offsetY = chunk.y * chunkSize; offsetY < (chunk.y + 1) * chunkSize; offsetY += density)
            {
                for (float offsetZ = chunk.z * chunkSize; offsetZ < (chunk.z + 1) * chunkSize; offsetZ += density)
                {                    
                    chunkContainer.transform.parent = container;
                    var obj = Instantiate(asteroids.Random(), new Vector3(offsetX, offsetY, offsetZ) + jitter.Random(true), Random.rotation, chunkContainer.transform);
                    //obj.AddForceAtPosition(drift.Random(true), Random.onUnitSphere, ForceMode.Impulse);                    
                }
            }
        }
    }
}
